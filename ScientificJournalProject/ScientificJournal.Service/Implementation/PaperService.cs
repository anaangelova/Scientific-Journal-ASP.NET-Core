using Microsoft.AspNetCore.Http;
using ScientificJournal.Domain.DomainModels;
using ScientificJournal.Domain.DTO;
using ScientificJournal.Domain.Identity;
using ScientificJournal.Repository.Interface;
using ScientificJournal.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScientificJournal.Service.Implementation
{
    public class PaperService : IPaperService
    {
        private readonly IPaperRepository paperRepository;
        private readonly IPapersKeywordsRepository papersKeywordsRepository;
        private readonly IUserRepository userRepository;
        private readonly IPapersUsersRepository papersUsersRepository;
        private readonly IPaperDocumentRepository paperDocumentRepository;

        public PaperService(IPaperRepository paperRepository, IPapersKeywordsRepository papersKeywordsRepository, IUserRepository userRepository, IPapersUsersRepository papersUsersRepository, IPaperDocumentRepository paperDocumentRepository)
        {
            this.paperRepository = paperRepository;
            this.papersKeywordsRepository = papersKeywordsRepository;
            this.userRepository = userRepository;
            this.papersUsersRepository = papersUsersRepository;
            this.paperDocumentRepository = paperDocumentRepository;
        }

        public void CreateNewPaper(PaperDTO p,IFormFile file)
        {
            //dopolnitelno treba da se zachuva i prateniot pdf dokument!!!
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }
            PaperDocument paperDocumentToAdd = new PaperDocument
            {
                Id=Guid.NewGuid(),
                DocumentName=file.FileName
            };
            //dodaj vo tabelata za PaperDocument
            paperDocumentRepository.Add(paperDocumentToAdd);

            //treba da kreira celosno popolnet objekt Paper, da go zachuva vo baza
            //isto taka treba da dodade konkretni objekti vo papersKeywords i vo papersUsers tabelite!
            Paper paperToAdd = new Paper
            {
                Id = Guid.NewGuid(),
                Title = p.Paper.Title,
                AreaOfResearch = p.Paper.AreaOfResearch,
                Abstract = p.Paper.Abstract,
                PaperDocumentId=paperDocumentToAdd.Id,
                PaperDocument=paperDocumentToAdd
           
                
            };
            paperRepository.Insert(paperToAdd);

            List<string> keywords = p.Keywords.Split(" ").ToList();
            foreach(string s in keywords)
            {
                PapersKeywords papersKeywordsToAdd = new PapersKeywords
                {
                    PaperId = paperToAdd.Id,
                    Keyword = s
                };
                papersKeywordsRepository.Add(papersKeywordsToAdd);
                //dodaj vo bazata za papersKeywords
            }

            List<string> authors = new List<string>();
            authors.Add(p.AuthorFirst);
            authors.Add(p.AuthorSecond);
            authors.Add(p.AuthorThird);
            foreach(string s in authors)
            {
                if (s != null)
                {
                    ScienceUser scienceUser = userRepository.GetByEmail(s);
                    PapersUsers itemToAdd = new PapersUsers
                    {
                        PaperId = paperToAdd.Id,
                        ScienceUserId = scienceUser.Id
                    };
                    papersUsersRepository.Add(itemToAdd);
                }
            }



        }

        public void DeletePaper(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Paper> GetAllPapers()
        {
            return paperRepository.GetAll().ToList();
        }

        public PaperDetailsDTO GetDetailsForPaper(Guid? id)
        {
            Paper paperToFind = paperRepository.Get(id);
           
            List<string> keywords = papersKeywordsRepository.FindKeywordsByPaper(id);
            StringBuilder stringBuilder = new StringBuilder();
            foreach(string k in keywords)
            {
                stringBuilder.Append(k+" ");

            }
            String keywordsToSend = stringBuilder.ToString();

            List<ScienceUser> authors = papersUsersRepository.GetAuthorsForPaper(id);

            //imeto na pdf dokumentot asociran so ovoj trud
            String documentName = paperToFind.PaperDocument.DocumentName;

            PaperDetailsDTO model = new PaperDetailsDTO
            {
                Paper = paperToFind,
                Keywords = keywordsToSend,
                Authors = authors,
                DocumentName=documentName
            };

            return model;
        }

        public void UpdateExistingPaper(Paper p)
        {
            throw new NotImplementedException();
        }
       
    }
}
