using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebASP_Library.Response
{
	public static class ApiResponse
	{
		public static async Task<BaseResponse<T>> ProcessApiResponse<T>(HttpResponseMessage response)
		{
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var value = await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
				return BaseResponse<T>.Success(value);
			}
			else
			{
				return BaseResponse<T>.Fail(response.StatusCode, response.StatusCode.ToString());
			}
		}
	}
}
