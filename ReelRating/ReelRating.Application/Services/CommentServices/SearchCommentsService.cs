using AutoMapper;
using Org.BouncyCastle.Asn1.Ocsp;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Core.Schema.CommentsSchema.Response;
using ReelRating.Data.Query.CommetsQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.CommentServices
{
    public class SearchCommentsService : ISearchCommentsService
    {
        private readonly IMapper _mapper;
        private readonly IListCommentsQuery _listCommentsQuery;
        private readonly IGetCommentByIdQuery _getCommentByIdQuery;

        public SearchCommentsService(IMapper mapper, IListCommentsQuery listCommentsQuery, IGetCommentByIdQuery getCommentByIdQuery)
        {
            _mapper = mapper;
            _listCommentsQuery = listCommentsQuery;
            _getCommentByIdQuery = getCommentByIdQuery;
        }

        public async Task<Result<CommentResponse>> SearchCommentById(SearchCommentByCustomerIdRequest request)
        {
            var result = new Result<CommentResponse>();
            try
            {
                var comment = await _getCommentByIdQuery.GetReviewByIdAndCustomerId(request.Id, request.CustomerId);
                if (comment == null)
                {
                    result.ValidateResult($"Comment with id '{request.Id}' not found.", 404);
                    return result;
                }
                result.SetSuccess(_mapper.Map<CommentResponse>(comment));
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }
        public async Task<Result<CommentResponse>> SearchCommentByIdAndCineId(SearchCommentByCustomerIdAndCineIdRequest request)
        {
            var result = new Result<CommentResponse>();
            try
            {
                var comment = await _getCommentByIdQuery.GetReviewByIdAndCustomerIdAndCineId(request.Id, request.CustomerId, request.CineId);
                if (comment == null)
                {
                    result.ValidateResult($"Comment with id '{request.Id}' not found.", 404);
                    return result;
                }
                result.SetSuccess(_mapper.Map<CommentResponse>(comment));
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }

        public async Task<PaginationResult<CommentResponse>> ListComments(ListCommentsRequest request)
        {
            var result = new PaginationResult<CommentResponse>();
            try
            {
                var comments = await _listCommentsQuery.ListComments(request.PageNumber, request.PageSize);
                
                result.SetSuccess(_mapper.Map<List<CommentResponse>>(comments), request.PageNumber, request.PageSize, comments.Count);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }

        public async Task<PaginationResult<CommentResponse>> ListCommentsById(ListCommentsByCustomerIdRequest request)
        {
            var result = new PaginationResult<CommentResponse>();
            try
            {
                var comments = await _listCommentsQuery.ListCommentsById(request.Id, request.PageNumber, request.PageSize);
                if (comments == null)
                {
                    result.ValidateResult($"List Comment with id '{request.Id}' not found.", 404);
                    return result;
                }

                result.SetSuccess(_mapper.Map<List<CommentResponse>>(comments), request.PageNumber, request.PageSize, comments.Count);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }
    }
}
