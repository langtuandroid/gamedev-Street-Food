using System;
using System.Collections.Generic;

namespace _Project.Scripts.Additional
{
	public static class Encryption
	{
		private static readonly string[] abc = new string[26];
		private static readonly string []stringToIntCompression = {"b","f","h","k","l","r","m","t","x","y"};
		private static readonly Int64 intAdd = 143;
		private static readonly Int64 intMultyplay = 7;

		private static Dictionary <string, Int64> _dic = new()  
		{
			{stringToIntCompression[0]  , 0},
			{stringToIntCompression[1]  , 1},
			{stringToIntCompression[2]  , 2},
			{stringToIntCompression[3]  , 3},
			{stringToIntCompression[4]  , 4},
			{stringToIntCompression[5]  , 5},
			{stringToIntCompression[6]  , 6},
			{stringToIntCompression[7]  , 7},
			{stringToIntCompression[8]  , 8},
			{stringToIntCompression[9]  , 9},
		};
	
		public static string Encrypt(string input)
		{
			Int64 inputDataInInt = 0;
			Int64.TryParse(input , out inputDataInInt);
			inputDataInInt = inputDataInInt*intMultyplay+intAdd;
			input = inputDataInInt.ToString();
			char []arrayChar = input.ToCharArray();
			string encryptionString  = "";
			for(int i = 0 ; i< arrayChar.Length ; i++)
			{
				string charArrayElementToString = arrayChar[i].ToString();
				Int64 charArrayElementToInt = 0;
				Int64.TryParse(charArrayElementToString, out charArrayElementToInt);
				encryptionString += stringToIntCompression[charArrayElementToInt];
			}
			int iCount = 0;
			for(char i = 'a'; i <= 'z' ;i++ )
			{
				abc[iCount] = i+""; 
				iCount++;
			}
			string[] extraChar = new string[3];
			for(int i = 0; i < 3 ; i++)
			{
				for( int j = 0; j < arrayChar.Length ; j++)
				{
					extraChar[i]+= abc[UnityEngine.Random.Range(0,26)];
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
				if(_dic.ContainsKey(charArrayElementToString))
				{
					Int64 correspondingInt = _dic[charArrayElementToString] ;
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
					decryptionInt = (decryptionInt-intAdd)/intMultyplay;
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