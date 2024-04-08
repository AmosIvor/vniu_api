using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories;
using vniu_api.Repositories.Products;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Services.Products
{
    public class ColourRepo : IColourRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ColourRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColourVM> CreateColourAsync(ColourVM colourVM)
        {
            var ColourMap = _mapper.Map<Colour>(colourVM);

            _context.Colours.Add(ColourMap);

            await _context.SaveChangesAsync();

            var newColourVM = _mapper.Map<ColourVM>(ColourMap);

            return newColourVM;
        }

        public async Task<ColourVM> GetColourByIdAsync(int colourId)
        {
            var Colour = await _context.Colours.SingleOrDefaultAsync(p => p.ColourId == colourId);

            return _mapper.Map<ColourVM>(Colour);
        }

        public async Task<ICollection<ColourVM>> GetColoursAsync()
        {
            var Colours = await _context.Colours.OrderBy(p => p.ColourId).ToListAsync();

            return _mapper.Map<ICollection<ColourVM>>(Colours);
        }

        public async Task<bool> IsColourExistIdAsync(int colourId)
        {
            return await _context.Colours.AnyAsync(p => p.ColourId == colourId);
        }

        public async Task<bool> IsColourExistNameAsync(string colourName)
        {
            var Colour = await _context.Colours.Where(p => p.ColourName.Trim().ToUpper() == colourName.TrimEnd().ToUpper())
                .FirstOrDefaultAsync();

            return Colour != null;
        }
    }
}