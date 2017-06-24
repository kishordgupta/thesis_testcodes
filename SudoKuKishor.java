package com.example;

import java.util.Scanner;

/**
 * Created by adminsiriconorchard on 6/22/17.
 */

public class SudoKuKishor {

    static boolean backTrack(short[][] arrMain, short[] curRow, short[] curCol,
                             short[] actRow, short[] actCol, int row, int col) {
        if(row>=curRow.length && col>=curCol.length) {
            for(int i = 0; i<curRow.length; i++) {
                if(curRow[i] != actRow[i]) {
                    return false;
                }
            }
            for(int j = 0; j<curCol.length; j++) {
                if(curCol[j] != actCol[j]) {
                    return false;
                }
            }
            return true;
        }
        if(col>=curCol.length) {
            return backTrack(arrMain, curRow, curCol, actRow, actCol, row+1, 0);
        } else if(row>=curRow.length) {
            return backTrack(arrMain, curRow, curCol, actRow, actCol, curRow.length, curCol.length);
        } else {
            if(curRow[row] < actRow[row] && curCol[col] < actCol[col]) {
                arrMain[row][col] = 1;
                curRow[row]++;
                curCol[col]++;
            }
            boolean ret = backTrack(arrMain, curRow, curCol, actRow, actCol, row, col+1);
            if(!ret && arrMain[row][col] == 1) {
                arrMain[row][col] = 0;
                curRow[row]--;
                curCol[col]--;
                return backTrack(arrMain, curRow, curCol, actRow, actCol, row, col+1);
            }
            return ret;
        }
    }

    static void solution() {
        Scanner in = new Scanner(System.in);
        int row = in.nextInt();
        int col = in.nextInt();
        short[][] arrMain = new short[row][col];
        short[] curRowSum = new short[row];
        short[] curColSum = new short[col];
        short[] actualRowSum = new short[row];
        short[] actualColSum = new short[col];
        for(int i = 0; i<row; i++) {
            actualRowSum[i] = in.nextShort();
        }
        for(int j = 0; j<col; j++) {
            actualColSum[j] = in.nextShort();
        }
        backTrack(arrMain, curRowSum, curColSum, actualRowSum, actualColSum, 0, 0);
        for(int i = 0; i<row; i++) {
            for(int j = 0; j<col; j++) {
                System.out.print(arrMain[i][j] == 1 ? "a ":"b ");
            }
            System.out.println();
        }
    }

    public static void main(String[] args) {
        solution();
    }
}
