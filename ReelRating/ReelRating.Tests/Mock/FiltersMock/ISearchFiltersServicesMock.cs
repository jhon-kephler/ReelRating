using Moq;
using ReelRating.Application.Services.HomeServices.Interfaces;
using ReelRating.Core.Schema;
using ReelRating.Core.Schema.FiltersSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Request;
using ReelRating.Core.Schema.HomeSchema.Response;

namespace ReelRating.Tests.Mock.FiltersMock;

public static class ISearchFiltersServicesMock
{
    public static Mock<ISearchFiltersServices> Create()
    {
        return new Mock<ISearchFiltersServices>();
    }

    public static Mock<ISearchFiltersServices> SetupGetCategories(this Mock<ISearchFiltersServices> mock, CategoriesRequest request, PaginationResult<CategoriesResponse> result)
    {
        mock.Setup(x => x.GetCategories(request)).ReturnsAsync(result);
        return mock;
    }

    public static Mock<ISearchFiltersServices> SetupGetYear(this Mock<ISearchFiltersServices> mock, YearRequest request, PaginationResult<int?> result)
    {
        mock.Setup(x => x.GetYear(request)).ReturnsAsync(result);
        return mock;
    }
}
