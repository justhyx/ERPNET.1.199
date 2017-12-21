using System;
using System.Collections.Generic;
using System.Web;

namespace ERPPlugIn.Class
{
    public static class GetAprove
    {
        public static bool getAprove(string User_Id, string aproveList)
        {
            List<string> AList = new List<string>();
            if (aproveList.IndexOf(";") != -1)
            {
                string[] sArray = aproveList.Split(';');
                foreach (string mailAddress in sArray)
                {
                    if (mailAddress.Trim() != string.Empty)
                    {

                        AList.Add(mailAddress);
                    }
                }
            }
            else
            {
                AList.Add(aproveList);
            }
            return AList.Contains(User_Id);
        }
    }
}
