using ArtikullServices.Models;
using ArtikullServices.Repository;
using AutoMapper;

namespace ArtikullServices.Services
{
    public class ProduktService
    {
        private ProduktRepository _produktRepository;
        private IMapper _mapper;
        public ProduktService(ProduktRepository produktRepository, IMapper mapper)
        {
            this._produktRepository = produktRepository;
            this._mapper = mapper;
        }
        public void addProdukt(Produkt produktEntity)
        {
            _produktRepository.addProdukt(produktEntity);
        }

        public IEnumerable<Produkt> GetProduktByName(string name)
        {
            return _produktRepository.GetProduktSearch(name);
        }

        public IEnumerable<Produkt> getProdukts()
        {
            return _produktRepository.getProdukt();
        }
        public Produkt getProductById(int id)
        {
            return _produktRepository.getProduktId(id);
        }

        public void deleteProdukt(int id)
        {
            _produktRepository.deleteProdukt(id);
        }
        public void EditProdukt(Produkt postCaseDto,int id)
        {
            _produktRepository.editProdukt(postCaseDto, id);
        }
    }
}
