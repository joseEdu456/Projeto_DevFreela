using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSucess = true, string message = "")
        {
            IsSucess = isSucess;
            Message = message;
        }

        public bool IsSucess { get; private set; }
        public string Message { get; private set; }

        public static ResultViewModel Sucess()
        {
            return new();
        }

        public static ResultViewModel Error(string mensagem)
        {
            return new(false, mensagem);
        }

    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool isSucess = true, string msg = "" ) : base(isSucess, msg)
        {
            Data = data;
        }

        public T? Data { get; private set; }

        public static ResultViewModel<T> Sucess(T data)
        {
            return new(data);
        }

        public static ResultViewModel<T> Error(string mensagem)
        {
            return new(default, false, mensagem);
        }
    }
}
