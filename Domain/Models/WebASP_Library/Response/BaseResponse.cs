using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebASP_Library.Response
{
	public class BaseResponse<T>
	{
		public string Description { get; set; } = string.Empty;
		public HttpStatusCode StatusCode { get; set; }
		public T Data { get; set; }

		public static BaseResponse<T> Success(T? data, string description = "")
		{
			return new BaseResponse<T> { StatusCode = HttpStatusCode.OK, Data = data, Description = description };
		}

		public static BaseResponse<T> Fail(HttpStatusCode responseStatus, string description)
		{
			return new BaseResponse<T> { StatusCode = responseStatus, Description = description };
		}

		public static BaseResponse<T> Error(string description)
		{
			return new BaseResponse<T> { StatusCode = HttpStatusCode.InternalServerError, Description = description };
		}
	}
}
