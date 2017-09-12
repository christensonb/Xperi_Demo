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
	public class Transfer
	{
	    public int transfer_id;
        public int user_id;
        public int deposit_account_id;
        public string deposit_account_name;
        public int withdraw_account_id;
        public string withdraw_account_name;
        public float amount;
        public string created_timestamp;
        public string receipt;

        public override string ToString()
        {
            string ret = "Transfer: ";
            ret += "transfer_id <'"+transfer_id.ToString()+"'> ";
            return ret;
        }

        public static Transfer Deserialize(string text, string encryption="", bool? includeLabel = null)
        {
            if (text == Crypto.NULL)
                return null;

            Transfer ret = new Transfer();
            Crypto crypto = new Crypto(text, encryption, includeLabel);
            ret.transfer_id = crypto.GetInt();
            ret.user_id = crypto.GetInt();
            ret.deposit_account_id = crypto.GetInt();
            ret.deposit_account_name = crypto.GetString();
            ret.withdraw_account_id = crypto.GetInt();
            ret.withdraw_account_name = crypto.GetString();
            ret.amount = crypto.GetFloat();
            ret.created_timestamp = crypto.GetString();
            ret.receipt = crypto.GetString();
            return ret;
        }

        public string Serialize(string encryption="", bool? includeLabel = null)
        {
            Crypto crypto = new Crypto("", encryption, includeLabel);
            crypto.AddInt(transfer_id,"transfer_id");
            crypto.AddInt(user_id,"user_id");
            crypto.AddInt(deposit_account_id,"deposit_account_id");
            crypto.AddString(deposit_account_name,"deposit_account_name");
            crypto.AddInt(withdraw_account_id,"withdraw_account_id");
            crypto.AddString(withdraw_account_name,"withdraw_account_name");
            crypto.AddFloat(amount,"amount");
            crypto.AddString(created_timestamp,"created_timestamp");
            crypto.AddString(receipt,"receipt");
            return crypto.ToString();
        }

        public static List<string> SerializeList(List< Transfer> objects, string encryption="", bool? includeLabel = null)
        {
            List<string> ret = new List<string>();
            foreach( Transfer child in objects)
                ret.Add(child.Serialize(encryption, includeLabel));
            return ret;
        }

        public static List<Transfer> DeserializeList(List<string> objects)
        {
            List<Transfer> ret = new List<Transfer>();
            foreach( string child in objects)
                ret.Add(Transfer.Deserialize(child));
            return ret;
        }
	}
}


