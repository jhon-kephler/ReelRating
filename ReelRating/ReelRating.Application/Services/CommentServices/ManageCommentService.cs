using AutoMapper;
using ReelRating.Application.Services.CommentServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CommentsSchema.Request;
using ReelRating.Data.Command.CommentCommand;
using ReelRating.Data.Query.CommetsQuery;
using ReelRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Application.Services.CommentServices
{
    public class ManageCommentService : IManageCommentService
    {
        private readonly IMapper _mapper;
        private readonly ICreateCommentCommand _createCommentCommand;
        private readonly IUpdateCommentCommand _updateCommentCommand;
        private readonly IGetCommentByIdQuery _getCommentByIdQuery;

        public ManageCommentService(IMapper mapper, ICreateCommentCommand createCommentCommand, IUpdateCommentCommand updateCommentCommand, IGetCommentByIdQuery getCommentByIdQuery)
        {
            _mapper = mapper;
            _createCommentCommand = createCommentCommand;
            _updateCommentCommand = updateCommentCommand;
            _getCommentByIdQuery = getCommentByIdQuery;
        }

        public async Task<Result> CreateComment(CreateCommentRequest request)
        {
            var result = new Result();
            try
            {
                var comment = _mapper.Map<Comments>(request);

                await _createCommentCommand.CreateAsync(comment);

                result.SetSuccess(true);
            }
            catch(Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }

            return result;
        }

        public async Task<Result> DeleteComment(DeleteCommentRequest request)
        {
            var result = new Result();
            try
            {
                var comment = await _getCommentByIdQuery.GetReviewById(request.Id);
                if (comment == null)
                {
                    result.ValidateResult($"Comment with id '{request.Id}' not found.", 404);
                    return result;
                }

                comment.Deleted = true;

                await _updateCommentCommand.UpdateCommets(request.Id, comment);

                result.SetSuccess(true);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred during the search: {ex.Message}");
            }
            return result;
        }
    }
}