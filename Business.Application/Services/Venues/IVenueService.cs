using Business.Application.DTOs.Venues;
using Xtech.Common.Pagination;

namespace Business.Application.Services.Venues
{
    public interface IVenueService
    {
        VenueDto CreateVenue(CreateVenueDto venueDto, int? artOrganizationId = null);
        VenueDto GetVenue(int id);
        PagedList<VenueDto> ListVenues(PagedListParams<VenueFilters> listParams);
        VenueDto EditVenue(int id, CreateVenueDto venueDto);
        void DeleteVenue(int id);
    }
}