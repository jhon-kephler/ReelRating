using AutoMapper;
using ReelRating.Application.Services.FavoritesServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FavoritesSchema.Request;
using ReelRating.Data.Command.FavoritesCommand;
using ReelRating.Data.Query.FavoritesQuery;
using ReelRating.Domain.Entities;

namespace ReelRating.Application.Services.FavoritesServices
{
    public class ManageFavoritesService : IManageFavoritesService
    {
        private readonly IMapper _mapper;
        private readonly ICreateFavoriteCommand _createFavoriteCommand;
        private readonly IUpdateFavoriteCommand _updateFavoriteCommand;
        private readonly IGetFavoriteQuery _getFavoriteQuery;

        public ManageFavoritesService(IMapper mapper, ICreateFavoriteCommand createFavoriteCommand, IUpdateFavoriteCommand updateFavoriteCommand, IGetFavoriteQuery getFavoriteQuery)
        {
            _mapper = mapper;
            _createFavoriteCommand = createFavoriteCommand;
            _updateFavoriteCommand = updateFavoriteCommand;
            _getFavoriteQuery = getFavoriteQuery;
        }

        public async Task<Result> CreateFavorite(CreateFavoriteRequest request)
        {
            var result = new Result();
            try
            {
                var favorite = await _getFavoriteQuery.GetFavoriteByCustomerIdAndCineId(request.CustomerId, request.CineId);
                if (favorite == null)
                {
                    var favoriteEntity = _mapper.Map<Favorites>(request);
                    await _createFavoriteCommand.AddFavorite(favoriteEntity);

                    result.SetSuccess(true);
                }
                else
                {
                    var favoriteEntity = _mapper.Map<Favorites>(request);
                    favoriteEntity.Deleted = false;

                    await _updateFavoriteCommand.UpdateFavorite(favorite.Id, favoriteEntity);

                    result.SetSuccess(true);
                }
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred while fetching categories: {ex.Message}");
            }
            return result;
        }

        public async Task<Result> DeleteFavorite(DeleteFavoriteRequest request)
        {
            var result = new Result();
            try
            {
                var favorite = await _getFavoriteQuery.GetFavoriteById(request.Id);
                if (favorite == null)
                {
                    result.ValidateResult($"Review with id '{request.Id}' not found.", 404);
                    return result;
                }

                favorite.Deleted = true;

                await _updateFavoriteCommand.UpdateFavorite(request.Id, favorite);

                result.SetSuccess(true);
            }
            catch (Exception ex)
            {
                result.ValidateResult($"An error occurred while fetching categories: {ex.Message}");
            }
            return result;
        }
    }
}
