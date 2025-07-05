

using EventHubApp.Web.ViewModels.Event;

namespace EventHubApp.Services.Core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<AllEventsIndexViewModel>> GetAllEventsAsync();

        //Task AddMovieAsync(MovieFormInputModel inputModel);

        //Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(string? id);

        //Task<MovieFormInputModel?> GetEditableMovieByIdAsync(string? id);

        //Task<bool> EditMovieAsync(MovieFormInputModel inputModel);

        //Task<DeleteMovieViewModel?> GetMovieDeleteDetailsByIdAsync(string? id);

        //Task<bool> SoftDeleteMovieAsync(string? id);

        //Task<bool> DeleteMovieAsync(string? id);
    }
}
