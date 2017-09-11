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
	public class Access
	{
	    public int access_id;
        public int account_id;
        public int user_id;

        public override string ToString()
        {
            string ret = "Access: ";
            ret += "access_id <'"+access_id.ToString()+"'> ";
            return ret;
        }

        public static Access Deserialize(string text, string encryption="", bool? includeLabel = null)
        {
            if (text == Crypto.NULL)
                return null;

            Access ret = new Access();
            Crypto crypto = new Crypto(text, encryption, includeLabel);
            ret.access_id = crypto.GetInt();
            ret.account_id = crypto.GetInt();
            ret.user_id = crypto.GetInt();
            return ret;
        }

        public string Serialize(string encryption="", bool? includeLabel = null)
        {
            Crypto crypto = new Crypto("", encryption, includeLabel);
            crypto.AddInt(access_id,"access_id");
            crypto.AddInt(account_id,"account_id");
            crypto.AddInt(user_id,"user_id");
            return crypto.ToString();
        }

        public static List<string> SerializeList(List< Access> objects, string encryption="", bool? includeLabel = null)
        {
            List<string> ret = new List<string>();
            foreach( Access child in objects)
                ret.Add(child.Serialize(encryption, includeLabel));
            return ret;
        }

        public static List<Access> DeserializeList(List<string> objects)
        {
            List<Access> ret = new List<Access>();
            foreach( string child in objects)
                ret.Add(Access.Deserialize(child));
            return ret;
        }
	}
}


