using System;
using System.Collections.Generic;

namespace _Project.Scripts.Additional
{
	public static class EncryptionHandler64
	{
		public static string[] alphabets = new string[26]; 
		public static string []encryptedStringCorrespondingToInt = {"b","f","h","k","l","r","m","t","x","y"}; 
		public static Int64 addInt = 143, multiplyInt = 7;
		public static Dictionary <string , Int64> myDic = new()  
		{
			{encryptedStringCorrespondingToInt[0]  , 0},
			{encryptedStringCorrespondingToInt[1]  , 1},
			{encryptedStringCorrespondingToInt[2]  , 2},
			{encryptedStringCorrespondingToInt[3]  , 3},
			{encryptedStringCorrespondingToInt[4]  , 4},
			{encryptedStringCorrespondingToInt[5]  , 5},
			{encryptedStringCorrespondingToInt[6]  , 6},
			{encryptedStringCorrespondingToInt[7]  , 7},
			{encryptedStringCorrespondingToInt[8]  , 8},
			{encryptedStringCorrespondingToInt[9]  , 9},
		};
	
		public static string Encrypt(string input)
		{
			Int64 inputDataInInt = 0;
			Int64.TryParse(input , out inputDataInInt);
			inputDataInInt = inputDataInInt*multiplyInt+addInt;
			input = inputDataInInt.ToString();
			char []arrayChar = input.ToCharArray();
			string encryptionString  = "";
			for(int i = 0 ; i< arrayChar.Length ; i++)
			{
				string charArrayElementToString = arrayChar[i].ToString();
				Int64 charArrayElementToInt = 0;
				Int64.TryParse(charArrayElementToString, out charArrayElementToInt);
				encryptionString += encryptedStringCorrespondingToInt[charArrayElementToInt];
			}
			int iCount = 0;
			for(char i = 'a'; i <= 'z' ;i++ )
			{
				alphabets[iCount] = i+""; 
				iCount++;
			}
			string[] extraChar = new string[3];
			for(int i = 0; i < 3 ; i++)
			{
				for( int j = 0; j < arrayChar.Length ; j++)
				{
					extraChar[i]+= alphabets[UnityEngine.Random.Range(0,26)];
				}
			}
			encryptionString = extraChar[0]+"-"+encryptionString+"-"+extraChar[1]+"-"+extraChar[2];
			return encryptionString;
		}

		public static Int64 Decrypt(string input)
		{
			string []splitted = new string[4];
			string decryptedString = "";
			splitted = input.Split('-');
			char []arrayChar = splitted[1].ToCharArray();
			for(int i = 0 ; i< arrayChar.Length ; i++)
			{
				string charArrayElementToString = arrayChar[i].ToString();
				if(myDic.ContainsKey(charArrayElementToString))
				{
					Int64 correspondingInt = myDic[charArrayElementToString] ;
					decryptedString+=correspondingInt;
				}
				else
				{
					decryptedString = "";
					break;
				}
			}
			Int64 decryptionInt = 0;
			if(decryptedString != "" && decryptedString != null)
			{
				if(arrayChar.Length > 19)
				{
					decryptionInt = 999999999999999999;
				}
				else
				{
					Int64.TryParse( decryptedString , out decryptionInt);
					decryptionInt = (decryptionInt-addInt)/multiplyInt;
				}
			}
			if(decryptionInt < 0)
			{
				decryptionInt = 0;
			}
			return decryptionInt;
		}

	}
}