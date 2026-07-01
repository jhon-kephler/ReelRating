using Moq;
using ReelRating.Application.Services.CineServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.CineSchema.Request;
using ReelRating.Core.Schema.CineSchema.Response;

namespace ReelRating.Tests.Mock.CineMock;

public static class ISearchCineServicesMock
{
    public static Mock<ISearchCineServices> Create()
    {
        return new Mock<ISearchCineServices>();
    }

    public static Mock<ISearchCineServices> SetupSearchCineDefault(this Mock<ISearchCineServices> mock, ListCineDefaultRequest request, PaginationResult<CineResponse> result)
    {
        mock.Setup(x => x.SearchCineDefault(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchCineServices> SetupSearchCineByName(this Mock<ISearchCineServices> mock, CineRequest request, Result<CineResponse> result)
    {
        mock.Setup(x => x.SearchCineByName(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchCineServices> SetupSearchCineByFilters(this Mock<ISearchCineServices> mock, ListCineByFiltersRequest request, PaginationResult<CineResponse> result)
    {
        mock.Setup(x => x.SearchCineByFilters(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchCineServices> SetupSearchCineByNameSuccess(this Mock<ISearchCineServices> mock, CineRequest request, CineResponse response)
    {
        var result = new Result<CineResponse>();
        result.SetSuccess(response);
        mock.Setup(x => x.SearchCineByName(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchCineServices> SetupSearchCineByNameError(this Mock<ISearchCineServices> mock, CineRequest request, string errorMessage)
    {
        var result = new Result<CineResponse>();
        result.ValidateResult(errorMessage);
        mock.Setup(x => x.SearchCineByName(request)).ReturnsAsync(result);
        return mock;
    }
}
