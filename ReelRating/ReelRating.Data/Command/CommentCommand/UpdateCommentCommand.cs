using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Command.CommentCommand
{
    public interface IUpdateCommentCommand { Task UpdateCommets(int id, Comments comments); }

    public class UpdateCommentCommand : IUpdateCommentCommand
    {
        private readonly IRepository<Comments> _repository;

        public UpdateCommentCommand(IRepository<Comments> repository)
        {
            _repository = repository;
        }

        public Task UpdateCommets(int id, Comments comments)
        {
            _repository.Update(id, comments);
            return Task.CompletedTask;
        }
    }
}
