using System;

namespace JadeLikeFairies.Services.Exceptions
{
    public class DeepValidationException : Exception
    {
        public DeepValidationException(string key, string error) 
            : base($"{key} : {error}")
        {
            Key = key;
            Error = error;
        }

        public string Key { get; set; }

        public string Error { get; set; }

    }
}