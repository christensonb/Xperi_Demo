/*
    This code is placeholder code for the developer to modify so as to call the rest call with
    parameters and to assign the callback function.

    So remove the placeholder in the namespace and modify to your hearts content

    This should be added to the REST game object.
*/

using UnityEngine;
using UnityEngine.UI;
using System;
using demo.BenChristenson;
using hg.ApiWebKit;
using hg.ApiWebKit.core.http;
using demo.BenChristenson.operations;
using System.Collections.Generic;

namespace demo.BenChristenson.placeholder
{
	public class BenChristensonCustomApi : MonoBehaviour
	{
	    private static BenChristensonCustomApi m_instance = null;
		private static BenChristenson benChristenson = null;
		private static BenChristensonApiInitialize benChristensonApiInitialize = null;
        public static int m_logLevel=1; // 0: no logging, 1: log on response error, 2: log on api errors, 3: log on error and success, 4: log on sent error and success
        

		public void Awake()
		{
            m_instance = this;
			benChristenson = GetComponent<BenChristenson> ();
			benChristensonApiInitialize = GetComponent<BenChristensonApiInitialize> ();
		}

        public static BenChristensonCustomApi Instance()
        {
            return m_instance;
        }

        public static string Prefix()
        {
            return Seaborn.Timestamp.Millisecond() + " API ";
        }

        

        public static void AccountTransferArrayGet(
            int account_id,                         // int of the account_id to get transfer for
            bool? withdraws_only = null,            // bool if true only gets withdraw transfer if false only gets deposit, default gets both
            int? offset = null,                     // int of the offset to use
            int? limit = null)                      // int of max number of puzzles to return
        {   
            if (benChristenson.accountTransferArrayGet == null){
                benChristenson.accountTransferArrayGet = m_instance.gameObject.AddComponent<behaviors.AccountTransferArrayGet>();
                benChristenson.accountTransferArrayGet.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountTransferArrayGet started");

            benChristenson.accountTransferArrayGet.Spawn(account_id, withdraws_only, offset, limit, 
                                       Callback: new Action<operations.AccountTransferArrayGet, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountTransferArrayGet failed " + response.Error);
                    }
                    else
                    {
                        List<models.Transfer> responseData = operation.responseData; // list of Transfer dict
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountTransferArrayGet completed Successfully with " + "List<models.Transfer> of size " + responseData.Count.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountTransferArrayGet failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void AccountTransferGet(
            int transfer_id)                        // int of the id for the transfer
        {   
            if (benChristenson.accountTransferGet == null){
                benChristenson.accountTransferGet = m_instance.gameObject.AddComponent<behaviors.AccountTransferGet>();
                benChristenson.accountTransferGet.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountTransferGet started");

            benChristenson.accountTransferGet.Spawn(transfer_id, 
                                       Callback: new Action<operations.AccountTransferGet, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountTransferGet failed " + response.Error);
                    }
                    else
                    {
                        models.Transfer responseData = operation.responseData;    // Transfer dict
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountTransferGet completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountTransferGet failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void AccountTransferPut(
            int withdraw_acount_id,                 // int of the account_id to withdraw the money from
            int deposit_account_id,                 // int of the account_id to deposit the moeny to
            float amount)                           // float of the amount to transfer
        {   
            if (benChristenson.accountTransferPut == null){
                benChristenson.accountTransferPut = m_instance.gameObject.AddComponent<behaviors.AccountTransferPut>();
                benChristenson.accountTransferPut.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountTransferPut started");

            benChristenson.accountTransferPut.Spawn(withdraw_acount_id, deposit_account_id, amount, 
                                       Callback: new Action<operations.AccountTransferPut, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountTransferPut failed " + response.Error);
                    }
                    else
                    {
                        models.Transfer responseData = operation.responseData;    // Transfer dict
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountTransferPut completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountTransferPut failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void AccountAccessArrayGet(
            int account_id)                         // int of the account_id for the account
        {   /*
			This will return all users who have access to the account, only the primary can do this command
			*/
            if (benChristenson.accountAccessArrayGet == null){
                benChristenson.accountAccessArrayGet = m_instance.gameObject.AddComponent<behaviors.AccountAccessArrayGet>();
                benChristenson.accountAccessArrayGet.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountAccessArrayGet started");

            benChristenson.accountAccessArrayGet.Spawn(account_id, 
                                       Callback: new Action<operations.AccountAccessArrayGet, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountAccessArrayGet failed " + response.Error);
                    }
                    else
                    {
                        List<models.User> responseData = operation.responseData;  // list of User dict
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountAccessArrayGet completed Successfully with " + "List<models.User> of size " + responseData.Count.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountAccessArrayGet failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void AccountAccessPut(
            int account_id,                         // int of the account_id for the account
            int user_id)                            // int of the user_id to grant access
        {   /*
			Only the primary on the account can add or remove user's access to an account
			*/
            if (benChristenson.accountAccessPut == null){
                benChristenson.accountAccessPut = m_instance.gameObject.AddComponent<behaviors.AccountAccessPut>();
                benChristenson.accountAccessPut.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountAccessPut started");

            benChristenson.accountAccessPut.Spawn(account_id, user_id, 
                                       Callback: new Action<operations.AccountAccessPut, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountAccessPut failed " + response.Error);
                    }
                    else
                    {
                        models.Access responseData = operation.responseData;      // Access dict
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountAccessPut completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountAccessPut failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void AccountAccessDelete(
            int account_id,                         // int of the account_id for the account
            int user_id)                            // int of the user_id to grant access
        {   /*
			Only the primary on the account can add or remove user's access to an account
			*/
            if (benChristenson.accountAccessDelete == null){
                benChristenson.accountAccessDelete = m_instance.gameObject.AddComponent<behaviors.AccountAccessDelete>();
                benChristenson.accountAccessDelete.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountAccessDelete started");

            benChristenson.accountAccessDelete.Spawn(account_id, user_id, 
                                       Callback: new Action<operations.AccountAccessDelete, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountAccessDelete failed " + response.Error);
                    }
                    else
                    {
                        models.Access responseData = operation.responseData;      // Access dict
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountAccessDelete completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountAccessDelete failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void AccountArrayGet()
        {   
            if (benChristenson.accountArrayGet == null){
                benChristenson.accountArrayGet = m_instance.gameObject.AddComponent<behaviors.AccountArrayGet>();
                benChristenson.accountArrayGet.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountArrayGet started");

            benChristenson.accountArrayGet.Spawn(
                                       Callback: new Action<operations.AccountArrayGet, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountArrayGet failed " + response.Error);
                    }
                    else
                    {
                        List<models.Account> responseData = operation.responseData; // list of Account dict the current user has access to
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountArrayGet completed Successfully with " + "List<models.Account> of size " + responseData.Count.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountArrayGet failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void AccountGet(
            int account_id)                         // int of the account to get
        {   
            if (benChristenson.accountGet == null){
                benChristenson.accountGet = m_instance.gameObject.AddComponent<behaviors.AccountGet>();
                benChristenson.accountGet.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountGet started");

            benChristenson.accountGet.Spawn(account_id, 
                                       Callback: new Action<operations.AccountGet, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountGet failed " + response.Error);
                    }
                    else
                    {
                        models.Account responseData = operation.responseData;     // Account dict of the account
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountGet completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountGet failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void AccountPut(
            string name = null,                     // str of name for the account, defaults to the created timestamp
            List<int> user_ids = null)              // list of int of users to give access to this account defaults to current user
        {   
            if (benChristenson.accountPut == null){
                benChristenson.accountPut = m_instance.gameObject.AddComponent<behaviors.AccountPut>();
                benChristenson.accountPut.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            if(user_ids != null && user_ids.Count == 0)
                user_ids = null;
            if(m_logLevel>3)
                Debug.Log(Prefix()+"AccountPut started");

            benChristenson.accountPut.Spawn(name, user_ids, 
                                       Callback: new Action<operations.AccountPut, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"AccountPut failed " + response.Error);
                    }
                    else
                    {
                        models.Account responseData = operation.responseData;     // Account dict created
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"AccountPut completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"AccountPut failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void EchoMessageGet(
            string message = "hello")               // str of the message to echo
        {   /*
			Very simple endpoint that just echos your message back to you
			*/
            if (benChristenson.echoMessageGet == null){
                benChristenson.echoMessageGet = m_instance.gameObject.AddComponent<behaviors.EchoMessageGet>();
                benChristenson.echoMessageGet.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"EchoMessageGet started");

            benChristenson.echoMessageGet.Spawn(message, 
                                       Callback: new Action<operations.EchoMessageGet, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"EchoMessageGet failed " + response.Error);
                    }
                    else
                    {
                        string responseData = operation.responseData;             // str of the message echoed
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"EchoMessageGet completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"EchoMessageGet failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void EchoGet()
        {   /*
			This will return the string "Hello World!"
			*/
            if (benChristenson.echoGet == null){
                benChristenson.echoGet = m_instance.gameObject.AddComponent<behaviors.EchoGet>();
                benChristenson.echoGet.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"EchoGet started");

            benChristenson.echoGet.Spawn(
                                       Callback: new Action<operations.EchoGet, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"EchoGet failed " + response.Error);
                    }
                    else
                    {
                        string responseData = operation.responseData;             // str of "Hello World!"
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"EchoGet completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"EchoGet failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void UserLoginEmailPost(
            string email,                           // str of the email as an alternative to username
            string password)                        // str of the password
        {   /*
			This will process logging the user in
			*/
            if (benChristenson.userLoginEmailPost == null){
                benChristenson.userLoginEmailPost = m_instance.gameObject.AddComponent<behaviors.UserLoginEmailPost>();
                benChristenson.userLoginEmailPost.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"UserLoginEmailPost started");

            benChristenson.userLoginEmailPost.Spawn(email, password, 
                                       Callback: new Action<operations.UserLoginEmailPost, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"UserLoginEmailPost failed " + response.Error);
                    }
                    else
                    {
                        models.User responseData = operation.responseData;        // User dict
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"UserLoginEmailPost completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"UserLoginEmailPost failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void UserLogoutPost()
        {   /*
			This will log the user out
			*/
            if (benChristenson.userLogoutPost == null){
                benChristenson.userLogoutPost = m_instance.gameObject.AddComponent<behaviors.UserLogoutPost>();
                benChristenson.userLogoutPost.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"UserLogoutPost started");

            benChristenson.userLogoutPost.Spawn(
                                       Callback: new Action<operations.UserLogoutPost, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"UserLogoutPost failed " + response.Error);
                    }
                    else
                    {
                        string responseData = operation.responseData;             // str of a message saying success
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"UserLogoutPost completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"UserLogoutPost failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void UserSignupPut(
            string username,                        // str of the username give my the player
            string password,                        // str of the password which will be hashed
            string email = null,                    // str of the user's email address
            string full_name = null)                // str of the full name
        {   /*
			This will create a PnP user
			*/
            if (benChristenson.userSignupPut == null){
                benChristenson.userSignupPut = m_instance.gameObject.AddComponent<behaviors.UserSignupPut>();
                benChristenson.userSignupPut.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"UserSignupPut started");

            benChristenson.userSignupPut.Spawn(username, password, email, full_name, 
                                       Callback: new Action<operations.UserSignupPut, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"UserSignupPut failed " + response.Error);
                    }
                    else
                    {
                        models.User responseData = operation.responseData;        // User dict of the user created
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"UserSignupPut completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"UserSignupPut failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void UserLoginPost(
            string username,                        // str of the username
            string password,                        // str of the password
            string email = null)                    // str of the email as an alternative to username
        {   /*
			This will process logging the user in
			*/
            if (benChristenson.userLoginPost == null){
                benChristenson.userLoginPost = m_instance.gameObject.AddComponent<behaviors.UserLoginPost>();
                benChristenson.userLoginPost.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"UserLoginPost started");

            benChristenson.userLoginPost.Spawn(username, password, email, 
                                       Callback: new Action<operations.UserLoginPost, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"UserLoginPost failed " + response.Error);
                    }
                    else
                    {
                        models.User responseData = operation.responseData;        // User dict
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"UserLoginPost completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"UserLoginPost failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void UserGet()
        {   
            if (benChristenson.userGet == null){
                benChristenson.userGet = m_instance.gameObject.AddComponent<behaviors.UserGet>();
                benChristenson.userGet.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"UserGet started");

            benChristenson.userGet.Spawn(
                                       Callback: new Action<operations.UserGet, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"UserGet failed " + response.Error);
                    }
                    else
                    {
                        models.User responseData = operation.responseData;        // User dict of the current user
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"UserGet completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"UserGet failed in response with: "+ex.ToString());
                }
            }));
        }
        

        public static void UserPost(
            string username = null,                 // str of the username give my the player
            string email = null,                    // str of the user's email address
            string full_name = null,                // str of the full name
            string password = null)                 // str of the password which will be hashed
        {   /*
			This will update a PnP user
			*/
            if (benChristenson.userPost == null){
                benChristenson.userPost = m_instance.gameObject.AddComponent<behaviors.UserPost>();
                benChristenson.userPost.DestroyOnComplete = benChristensonApiInitialize.is_behavior_destroyed_on_complete();
            }

            
            if(m_logLevel>3)
                Debug.Log(Prefix()+"UserPost started");

            benChristenson.userPost.Spawn(username, email, full_name, password, 
                                       Callback: new Action<operations.UserPost, HttpResponse> ((operation, response) =>
            {
                try
                {
                    if (response.HasError)
                    {
                        if(m_logLevel>1)
                            Debug.LogError(Prefix()+"UserPost failed " + response.Error);
                    }
                    else
                    {
                        models.User responseData = operation.responseData;        // User dict of the user updated
                        if(m_logLevel>2)
                            Debug.Log(Prefix()+"UserPost completed Successfully with " + responseData.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if(m_logLevel>0)
                        Debug.LogError(Prefix()+"UserPost failed in response with: "+ex.ToString());
                }
            }));
        }
        
    }
}


