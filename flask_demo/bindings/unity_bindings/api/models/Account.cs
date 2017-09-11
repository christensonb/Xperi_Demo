/*
    This file contains models classes that the json response data will be mapped to
*/

using UnityEngine;
using System;
using System.Collections.Generic;
using hg.ApiWebKit.core.http;
using hg.ApiWebKit.core.attributes;
using hg.ApiWebKit.mappers;
using Seaborn;

namespace demo.BenChristenson.models
{
	public class Account
	{
	    public string name;
        public float funds;
        public List<int> user_ids;

        public override string ToString()
        {
            string ret = "Account: ";
            ret += "name <'"+name.ToString()+"'> ";
            return ret;
        }

        public static Account Deserialize(string text, string encryption="", bool? includeLabel = null)
        {
            if (text == Crypto.NULL)
                return null;

            Account ret = new Account();
            Crypto crypto = new Crypto(text, encryption, includeLabel);
            ret.name = crypto.GetString();
            ret.funds = crypto.GetFloat();
            ret.user_ids = crypto.GetListInt();
            return ret;
        }

        public string Serialize(string encryption="", bool? includeLabel = null)
        {
            Crypto crypto = new Crypto("", encryption, includeLabel);
            crypto.AddString(name,"name");
            crypto.AddFloat(funds,"funds");
            crypto.AddListInt(user_ids,"user_ids");
            return crypto.ToString();
        }

        public static List<string> SerializeList(List< Account> objects, string encryption="", bool? includeLabel = null)
        {
            List<string> ret = new List<string>();
            foreach( Account child in objects)
                ret.Add(child.Serialize(encryption, includeLabel));
            return ret;
        }

        public static List<Account> DeserializeList(List<string> objects)
        {
            List<Account> ret = new List<Account>();
            foreach( string child in objects)
                ret.Add(Account.Deserialize(child));
            return ret;
        }
	}
}


