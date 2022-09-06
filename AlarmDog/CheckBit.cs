using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmDog
{
    class CheckBit
    {
        public string CalculateCheckBit(string Account) 
        {
            int[] m1 = new int[5], m2 = new int[14];
            int j;
            string CheckedAccount, MFO;
            //m1
            m1[0] = 1; m1[1] = 3; m1[2] = 7; m1[3] = 1; m1[4] = 3;
            //m2
            m2[0] = 3; m2[1] = 7; m2[2] = 1; m2[3] = 3; m2[4] = 7;
            m2[5] = 1; m2[6] = 3; m2[7] = 7; m2[8] = 1; m2[9] = 3;
            m2[10] = 7; m2[11] = 1; m2[12] = 3; m2[13] = 7;
            //MFO
            MFO = "820172";
            while (MFO.Length < 6)
            {
                MFO = MFO + "0";
            }
            while (Account.Length < 5)
            {
                Account = Account + "0";
            }
            Account = Account.TrimStart(null);
            CheckedAccount = "";

            for (int i = 0; i <= Account.Length - 1; i++)
            {
                if (Account.Substring(i, 1) == " ")
                {
                    return "0";
                }
                if (Int32.Parse(Account.Substring(i, 1)) < 0 | Int32.Parse(Account.Substring(i, 1)) > 9)
                {
                    if (i < 5)
                    {
                        CheckedAccount = CheckedAccount + "0";
                    }
                    else
                    {
                        CheckedAccount = CheckedAccount + " ";
                    }
                }
                else
                {
                    CheckedAccount = CheckedAccount + Account.Substring(i, 1);
                }
            }
            CheckedAccount = CheckedAccount.Remove(4, 1);
            CheckedAccount = CheckedAccount.Insert(4, "0");
            CheckedAccount = CheckedAccount.Trim() + " ";
            CheckedAccount = CheckedAccount.Substring(0, CheckedAccount.IndexOf(" "));
            j = 0;

            for (int i = 0; i <= 4; i++)
            {
                j = j + Int32.Parse(MFO.Substring(i, 1)) * m1[i];
            }
            for (int i = 0; i <= CheckedAccount.Length - 1; i++)
            {
                j = j + Int32.Parse(CheckedAccount.Substring(i,1)) * m2[i];
            }
            string CheckBit = (((j+ CheckedAccount.Length) * 7)%10).ToString();
            if (CheckBit.Length != 1)
            {
                CheckBit = CheckBit.Substring(1, 1);
            }
            CheckedAccount = CheckedAccount.Remove(4, 1);
            CheckedAccount = CheckedAccount.Insert(4, CheckBit);

            return CheckedAccount;
            
        }
    }
}
