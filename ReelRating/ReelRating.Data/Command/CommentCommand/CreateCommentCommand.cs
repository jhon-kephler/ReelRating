using ReelRating.Data.Command.AuthCommand;
using ReelRating.Domain.Entities;
using ReelRating.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReelRating.Data.Command.CommentCommand
{
    public interface ICreateCommentCommand { Task<bool> CreateAsync(Comments comments);  }

    public class CreateCommentCommand : ICreateCommentCommand
    {
        private IRepository<Comments> _repository;

        public CreateCommentCommand(IRepository<Comments> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(Comments comments)
        {
            _repository.Add(comments);
            return await Task.FromResult(true);
        }
    }
}
