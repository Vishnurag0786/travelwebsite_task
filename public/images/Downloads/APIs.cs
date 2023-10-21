using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Prorata_Main_Web.Models
{
    public class APIs
    {
        public string CustomerEnquiry(string FirstName, string LastName, string Mobile, string Email, string City, string Apartment, string CurrentCar, string InterestedCar)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.prorataresidence.com/api/ProrataCar/CustomerEnquiryWeb");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Mobile = Mobile,
                    Email = Email,
                    City = City,
                    Apartment = Apartment,
                    CurrentCar = CurrentCar,
                    InterestedCar = InterestedCar
                });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        public string CRMCustomerAPI(string first_name, string last_name, string mobile_number, string email, string owner_id, string cf_currentcity, string cf_apartment, string cf_current_car, string cf_interest_car)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://prorata.myfreshworks.com/crm/sales/api/contacts");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.PreAuthenticate = true;
            httpWebRequest.Headers.Add("Authorization", "Token token=VckirG9a9JFAv3ajjiBHdA");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    contact = new
                    {
                        first_name = first_name,
                        last_name = last_name,
                        mobile_number = mobile_number,
                        email = email,
                        owner_id = owner_id,

                        custom_field = new
                        {
                            cf_currentcity = cf_currentcity,
                            cf_apartment = cf_apartment,
                            cf_current_car = cf_current_car,
                            cf_interest_car = cf_interest_car
                        }
                    }
                });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }



        #region Bluewaves SMS API
        public void SendSMSViaBluewaves(string sendTo, string smsText, string smsTemplateId, string smsHeaderName)
        {
            try
            {
                sendTo = "91" + sendTo.Trim();
                string SMSURL = string.Empty;
                SMSURL = UserConstants.BLUEWAVES_SMS_API_2.Replace("{0}", smsHeaderName);
                SMSURL = SMSURL.Replace("{1}", sendTo);
                SMSURL = SMSURL.Replace("{2}", smsText);
                SMSURL = SMSURL.Replace("{3}", UserConstants.SMS_DLT_ENTITY_ID);
                SMSURL = SMSURL.Replace("{4}", smsTemplateId);
                //SMSURL = String.Format(SMSURL, sendTo, smsText);


                StringBuilder postData = new StringBuilder();
                string responseMessage = string.Empty;
                HttpWebRequest request = null;

                request = (HttpWebRequest)WebRequest.Create(SMSURL);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Read the response
                    using (StreamReader srResponse = new StreamReader(response.GetResponseStream()))
                    {
                        responseMessage = srResponse.ReadToEnd();
                    }

                    // Logic to interpret response from your gateway goes here
                    // Response.Write(String.Format("Response from gateway: {0}", responseMessage));
                }
            }
            catch (Exception _exObj)
            {
                // Response.Write(objException.ToString());
            }
        }
        #endregion



    }
}