using ReelRating.Core.Schema.AuthSchema.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ReelRating.Core.Schema
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        private int? _statusCode;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int StatusCode
        {
            get => _statusCode ?? (IsSuccess ? 200 : 500);
            set => _statusCode = value;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public void ValidateResult(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
        public void ValidateResult(string errorMessage, int status)
        {
            IsSuccess = false;
            StatusCode = status;
            ErrorMessage = errorMessage;
        }

        public void SetSuccess(T data)
        {
            IsSuccess = true;
            Data = data;
        }

    }

    public class Result : Result<object>
    {
        public Result() { }
    }

    public class PaginationResult<T> : Result<List<T>>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PageNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PageSize { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TotalItems { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;

        public void SetSuccess(List<T> items, int pageNumber, int pageSize, int totalItems)
        {
            IsSuccess = true;
            Data = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
    }
}
