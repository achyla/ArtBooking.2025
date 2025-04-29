using Business.Application.DTOs.Venues;
using Business.Model.Data;
using Business.Model.Entities.Venues;
using Xtech.Common.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Business.Application.Services.Venues
{
    public class VenueService : IVenueService
    {
        private readonly ArtBookingDbContext _dbContext;
        private readonly IMapper _mapper;

        public VenueService(ArtBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public VenueDto CreateVenue(CreateVenueDto venueDto, int? artOrganizationId = null)
        {
            var venue = _mapper.Map<Venue>(venueDto);
            venue.CreatedAt = DateTime.UtcNow;
            venue.ArtOrganizationId = artOrganizationId ?? 0; // Set to 0 if null

            _dbContext.Venues.Add(venue);
            _dbContext.SaveChanges();

            return _mapper.Map<VenueDto>(venue);
        }

        public VenueDto GetVenue(int id)
        {
            var venue = _dbContext.Venues.Find(id);
            return venue != null ? _mapper.Map<VenueDto>(venue) : null;
        }

        public PagedList<VenueDto> ListVenues(PagedListParams<VenueFilters> listParams)
        {
            var query = _dbContext.Venues.AsQueryable();

            if (listParams.Filters != null)
            {
                if (!string.IsNullOrEmpty(listParams.Filters.Name))
                {
                    query = query.Where(v => v.Name.ToLower().Contains(listParams.Filters.Name.ToLower()));
                }

                if (!string.IsNullOrEmpty(listParams.Filters.Description))
                {
                    query = query.Where(v => v.Description.ToLower().Contains(listParams.Filters.Description.ToLower()));
                }
            }

            if (listParams.HasSort())
            {
                if (listParams.SortByFieldIs("Name"))
                {
                    query = listParams.IsSortByAsc()
                        ? query.OrderBy(v => v.Name)
                        : query.OrderByDescending(v => v.Name);
                }
                else
                {
                    query = query.OrderBy(v => v.Name);
                }
            }
            else
            {
                query = query.OrderBy(v => v.Name);
            }

            var pagedList = query.AsPagedList(listParams.PageNumber, listParams.PageSize);
            return new PagedList<VenueDto>(_mapper.Map<List<VenueDto>>(pagedList.Items), pagedList.TotalCount, pagedList.PageNumber, pagedList.PageSize);
        }

        public VenueDto EditVenue(int id, CreateVenueDto venueDto)
        {
            var existingVenue = _dbContext.Venues.Find(id);
            if (existingVenue == null)
            {
                return null;
            }

            _mapper.Map(venueDto, existingVenue);
            _dbContext.SaveChanges();

            return _mapper.Map<VenueDto>(existingVenue);
        }

        public void DeleteVenue(int id)
        {
            var venue = _dbContext.Venues.Find(id);
            if (venue != null)
            {
                _dbContext.Venues.Remove(venue);
                _dbContext.SaveChanges();
            }
        }
    }
}