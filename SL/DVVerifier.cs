using BE.Entities;
using DAL.Mappers;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace SL
{
    public class DVVerifier
    {
        private IList<string> _dvvMembers;
        protected static readonly ILog Log = LogManager.GetLogger(GlobalValues.WebLoggerName);

        public DVVerifier()
        {
            SetDvvMembers();
        }

        private void SetDvvMembers()
        {
            _dvvMembers = new List<string>
            {
                "UserDAL"
            };
        }

        public bool DVVerify()
        {
            var result = true;

            foreach (var dvvMember in _dvvMembers)
            {
                var dataList = Invoke(dvvMember, "GetDVHEntities");
                if (dataList.Any())
                {
                    var hashDVV = DVVCalculate(dataList);

                    if (!DVVVerify(hashDVV, dvvMember))
                    {
                        Log.Fatal("Se encontro inconsistencia en base de datos en: " + dvvMember);

                        BinnacleSL binnacleSL = new BinnacleSL();

                        binnacleSL.AddBinnacle(new BinnacleBE()
                        {
                            Description = "Se encontro inconsistencia en base de datos en: " + dvvMember
                        });
                        foreach (var obj in dataList)
                        {
                            var hashDVH = DVHCalculate(obj);
                            if (!DVHVerify(obj.GetType().GetProperty("DVH").GetValue(obj).ToString(), hashDVH))
                            {
                                binnacleSL.AddBinnacle(new BinnacleBE()
                                {
                                    Description = "El número de registro modificado es: " + dataList.IndexOf(obj).ToString()
                                });
                                Log.Fatal("El número de registro modificado es: " + dataList.IndexOf(obj).ToString());
                                result = false;
                            }
                        }
                    }
                }

            }

            return result;
        }










        private IList<object> Invoke(string typeName, string methodName)
        {
            var assembly = Assembly.Load("DAL");
            var type = assembly.GetType("DAL.Mappers." + typeName);
            var instance = Activator.CreateInstance(type);
            var method = type.GetMethod(methodName);
            var result = (IList)method.Invoke(instance, null);

            return result.Cast<object>().ToList();
        }

        public void DVCalculate()
        {
            foreach (var dvvMember in _dvvMembers)
            {
                var dataList = Invoke(dvvMember, "GetDVHEntities");

                if (dataList.Any())
                {
                    var hash = DVVCalculate(dataList);
                    var dvv = new DVVBE
                    {
                        TableName = dvvMember,
                        DVVHash = hash
                    };

                    if (!(new DVDAL().Update(dvv)))
                        Log.Error("El digito vertical no se guardó correctamente");
                }
            }
        }

        public void DVCalculate(string dvvMember)
        {
           
            var dataList =Invoke(dvvMember, "GetDVHEntities");

            if (dataList.Any())
            {
                var hash = DVVCalculate(dataList);
                var dvv = new DVVBE
                {
                    TableName = dvvMember,
                    DVVHash = hash
                };

                if (!(new DVDAL().Update(dvv)))
                    Log.Error("El digito vertical no se guardó correctamente");
            }
            
        }


        //public  DVCalculateAndSave(string dvvMember)
        //{

           

            
        //        var hash = DVVCalculate(dataList);
        //        var dvv = new DVVBE
        //        {
        //            TableName = dvvMember,
        //            DVVHash = hash
        //        };

        //        if (!(new DVDAL().Update(dvv)))
        //            Log.Error("El digito vertical no se guardó correctamente");
        //    }
        //}


        

        public string DVHCalculate(object obj)
        {
            var stringBilder = new StringBuilder();

            foreach (var prop in obj.GetType().GetProperties())
            {
                if (!prop.Name.Equals("DVH"))
                    stringBilder.Append(prop.GetValue(obj));
            }

            return new Encryptor().Encrypt(stringBilder.ToString());
        }

        private string DVVCalculate<T>(IList<T> objs)
        {
            var stringBuilder = new StringBuilder();

            foreach (var obj in objs)
            {
                var dvh = DVHCalculate(obj);
                stringBuilder.Append(dvh);
            }

            return new Encryptor().Encrypt(stringBuilder.ToString());
        }

        private bool DVVVerify(string hashString, string tableName)
        {
            var dvvList = new DVDAL().Get();
            var stringOrigin = dvvList.FirstOrDefault(x => x.TableName == tableName)?.DVVHash;

            if (string.IsNullOrEmpty(stringOrigin))
                return true;

            return new Encryptor().CheckEncryption(stringOrigin, hashString);
        }

        private bool DVHVerify(string stringOrigin, string stringToCompare)
        {
            return new Encryptor().CheckEncryption(stringOrigin, stringToCompare);
        }
    }
}
