using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAdministrator.DAL;
using WebAdministrator.Models;

namespace WebAdministrator.Util
{
    public static class Utility
    {
        public static readonly string Secret = "eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZX0=";

        public static string  GenerateToken(Usuario userCurrency)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userCurrency.Nome.ToString()),
                    new Claim(ClaimTypes.Email, userCurrency.Email.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(1).ToString("HH:mm:ss"))

                    }),
                    //Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };


                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static SelectList GetSigla()
        {
            
            List<KeyValuePair<string, string>> listEstadoSigla = new List<KeyValuePair<string, string>>{
                
                new KeyValuePair<string, string>("AC","AC - Acre")
                ,new KeyValuePair<string, string>("AL","AL - Alagoas")
                ,new KeyValuePair<string, string>("AP","AP - Amapá")
                ,new KeyValuePair<string, string>("AM","AM - Amazonas")
                ,new KeyValuePair<string, string>("BA","BA - Bahia")
                ,new KeyValuePair<string, string>("CE","CE - Ceará")
                ,new KeyValuePair<string, string>("DF","DF - Distrito Federal")
                ,new KeyValuePair<string, string>("ES","ES - Espírito Santo")
                ,new KeyValuePair<string, string>("GO","GO - Goiás")
                ,new KeyValuePair<string, string>("MA","MA - Maranhão")
                ,new KeyValuePair<string, string>("MT","MT - Mato Grosso")
                ,new KeyValuePair<string, string>("MS","MS - Mato Grosso do Sul")
                ,new KeyValuePair<string, string>("MG","MG - Minas Gerais")
                ,new KeyValuePair<string, string>("PA","PA - Pará")
                ,new KeyValuePair<string, string>("PB","PB - Paraíba")
                ,new KeyValuePair<string, string>("PR","PR - Paraná")
                ,new KeyValuePair<string, string>("PE","PE - Pernambuco")
                ,new KeyValuePair<string, string>("PI","PI - Piauí")
                ,new KeyValuePair<string, string>("RJ","RJ - Rio de Janeiro")
                ,new KeyValuePair<string, string>("RN","RN - Rio Grande do Norte")
                ,new KeyValuePair<string, string>("RS","RS - Rio Grande do Sul" )
                ,new KeyValuePair<string, string>("RO","RO - Rondônia")
                ,new KeyValuePair<string, string>("RR","RR - Roraima")
                ,new KeyValuePair<string, string>("SC","SC - Santa Catarina")
                ,new KeyValuePair<string, string>("SP","SP - São Paulo" )
                ,new KeyValuePair<string, string>("SE","SE - Sergipe")
                ,new KeyValuePair<string, string>("TO","TO - Tocantins")
            };


            return new SelectList(listEstadoSigla.Select(p => new SelectListItem { Value = p.Key.ToString(), Text = p.Value }).AsEnumerable(),"Value", "Text");
        }
        public static List<T> GetSelectLists<T>(Expression<Func<T, bool>> predicate) where T: class
        {
            try
            {
                using DbCon db = new DbCon();
                using var contexto = new RepositoryGeneric<T>(db, out GenericRepositoryValidation.GenericRepositoryExceptionStatus status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                    return contexto.GetAll(predicate).ToList();
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Criptografar(string _input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(_input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }
    }
}
