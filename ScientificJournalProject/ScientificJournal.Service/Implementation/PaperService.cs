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
        private readonly IConferenceRepository conferenceRepository;

        public PaperService(IPaperRepository paperRepository, IPapersKeywordsRepository papersKeywordsRepository, IUserRepository userRepository, IPapersUsersRepository papersUsersRepository, IPaperDocumentRepository paperDocumentRepository, IConferenceRepository conferenceRepository)
        {
            this.paperRepository = paperRepository;
            this.papersKeywordsRepository = papersKeywordsRepository;
            this.userRepository = userRepository;
            this.papersUsersRepository = papersUsersRepository;
            this.paperDocumentRepository = paperDocumentRepository;
            this.conferenceRepository = conferenceRepository;
        }

        public void CreateNewPaper(PaperDTO p,IFormFile file)
        {
       
                PaperDocument paperDocumentToAdd = new PaperDocument
            {
                Id = Guid.NewGuid(),
                
            };
            string trimmedName = file.FileName.Replace(".pdf", paperDocumentToAdd.Id.ToString());
            string finalName = trimmedName + ".pdf";
            paperDocumentToAdd.DocumentName = finalName;
      

            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{paperDocumentToAdd.DocumentName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }
           

            Conference conferenceFromView = conferenceRepository.GetConferenceById(p.ConferenceId);
            Conference conferenceForPaper = new Conference
            {
                ConferenceName = conferenceFromView.ConferenceName
            };
            conferenceRepository.Insert(conferenceForPaper);

            Paper paperToAdd = new Paper
            {
                Id = Guid.NewGuid(),
                Title = p.Paper.Title,
                AreaOfResearch = p.Paper.AreaOfResearch,
                Abstract = p.Paper.Abstract,
                PaperDocumentId=paperDocumentToAdd.Id,
                PaperDocument=paperDocumentToAdd,
                status=Status.PENDING,
                Conference = conferenceForPaper,
                ConferenceId = conferenceForPaper.Id


            };
            
            List<PapersKeywords> keywordsForPaper = new List<PapersKeywords>();
            List<string> keywords = p.Keywords.Trim(',').Split(" ").ToList();
            foreach(string s in keywords)
            {
                PapersKeywords papersKeywordsToAdd = new PapersKeywords
                {
                    PaperId = paperToAdd.Id,
                    Keyword = s
                };
                keywordsForPaper.Add(papersKeywordsToAdd);
        
            }
            paperToAdd.Keywords = keywordsForPaper;

            List<PapersUsers> papersUsersList = new List<PapersUsers>();
            List<string> authors = new List<string>();
      
            authors.Add(p.AuthorFirst);
            authors.Add(p.AuthorSecond);
            authors.Add(p.AuthorThird);
            foreach(string s in authors)
            {
                if (s != null)
                {
                    ScienceUser scienceUser = userRepository.GetByEmail(s);
                    PapersUsers itemToAdd;
                    if (scienceUser == null) //ne postoi korisnikot vo bazata
                    {
                        scienceUser = new ScienceUser
                        {
                            Email = s,
                        };
                        userRepository.Insert(scienceUser);
                        itemToAdd = new PapersUsers
                        {
                            PaperId = paperToAdd.Id,
                            ScienceUserId = scienceUser.Id
                        };
                    }
                    else
                    {
                        itemToAdd = new PapersUsers
                        {
                            PaperId = paperToAdd.Id,
                            ScienceUserId = scienceUser.Id
                        };

                    }
                   
                    papersUsersList.Add(itemToAdd);
                
                }
            }
            paperToAdd.AuthorsForPaper = papersUsersList;
            paperRepository.Insert(paperToAdd);

        }

       

        public List<Paper> GetAllPapers() //only approved papers
        {
            return paperRepository.GetAll().ToList();
        }

        private String GetKeywordsAsString(List<string> keywordsList)
        {
            
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string k in keywordsList)
            {
                stringBuilder.Append(k + " ");

            }
            
            String keywordsToSend = stringBuilder.ToString();
            keywordsToSend = keywordsToSend.TrimStart().TrimEnd();
          
            return keywordsToSend;
        }
        public PaperDetailsDTO GetDetailsForPaper(Guid? id)
        {
            Paper paperToFind = paperRepository.Get(id); 
            
            List<string> keywordsList = paperToFind.Keywords.Select(pk => pk.Keyword).ToList();
            
            List<ScienceUser> authorsList = paperToFind.AuthorsForPaper.Select(pa => pa.ScienceUser).ToList();
            PaperDetailsDTO model = new PaperDetailsDTO
            {
                Paper = paperToFind,
                Keywords = keywordsList, 
                Authors = authorsList,
                DocumentId=paperToFind.PaperDocumentId,
                ConferenceName=paperToFind.Conference.ConferenceName
            };

            return model;
        }
        public PaperDTO GetDetailsForEdit(Guid? id, ScienceUser currentUser)
        {
            PaperDetailsDTO tmp = this.GetDetailsForPaper(id);
            String keywordsAsString = this.GetKeywordsAsString(tmp.Keywords);
            ScienceUser firstAuthor = tmp.Authors.Where(a => a.Equals(currentUser)).FirstOrDefault();
            List<ScienceUser> tmpList= tmp.Authors.Where(a => !a.Equals(currentUser)).ToList();
            PaperDTO dto= new PaperDTO
            {
                Paper = tmp.Paper,
                Keywords = keywordsAsString,
                AuthorFirst = firstAuthor.Email,
                AuthorSecond = tmpList.ElementAtOrDefault(0) != null ? tmpList.ElementAt(0).Email : "",
                AuthorThird = tmpList.ElementAtOrDefault(1) != null ? tmpList.ElementAt(1).Email : "",
                ConferenceName = tmp.ConferenceName,
                ConferenceId = tmp.Paper.ConferenceId,
                Conferences = conferenceRepository.GetAllConferences()
            };
            return dto;
          

        }

        public void UpdateExistingPaper(PaperDTO p)
        {
            Paper paperToUpdate = paperRepository.Get(p.Paper.Id);

            List<PapersKeywords> paperKeywordsList = new List<PapersKeywords>();
            List<string> keywords = p.Keywords.Trim(',').Split(" ").ToList();

            foreach (string s in keywords)
            {
                PapersKeywords papersKeywordsToAdd = new PapersKeywords
                {
                    PaperId = p.Paper.Id,
                    Keyword = s,
                    Paper=p.Paper
                };
                
                paperKeywordsList.Add(papersKeywordsToAdd);
           
            }
            paperToUpdate.Keywords = paperKeywordsList;

            List<string> authors = new List<string>();
            List<PapersUsers> papersUsersList = new List<PapersUsers>();
            authors.Add(p.AuthorFirst);
            authors.Add(p.AuthorSecond);
            authors.Add(p.AuthorThird);
            foreach (string s in authors)
            {
                if (s != null && s!="")
                {
                    ScienceUser scienceUser = userRepository.GetByEmail(s);
                    PapersUsers itemToAdd = new PapersUsers
                    {
                        PaperId =p.Paper.Id,
                        Paper=p.Paper,
                        ScienceUser=scienceUser,
                        ScienceUserId = scienceUser.Id
                    };
                    papersUsersList.Add(itemToAdd);
                  
                   
                }
            }
            paperToUpdate.AuthorsForPaper = papersUsersList;
            paperToUpdate.status = Status.PENDING;
            paperToUpdate.Abstract = p.Paper.Abstract;
            paperToUpdate.Title = p.Paper.Title;
            paperToUpdate.AreaOfResearch = p.Paper.AreaOfResearch;

            Conference conferenceFromView = conferenceRepository.GetConferenceById(p.ConferenceId);
            Conference toFind = new Conference
            {
                ConferenceName = conferenceFromView.ConferenceName
            };
            conferenceRepository.Insert(toFind);

            paperToUpdate.Conference = toFind;
            paperToUpdate.ConferenceId = toFind.Id;

            paperRepository.Update(paperToUpdate);
            
        }
        public void DeletePaper(Guid id)
        {
            Paper paperToDelete = this.GetDetailsForPaper(id).Paper;
            paperRepository.Delete(paperToDelete);
        }

        public List<Paper> GetPapersForUser(string userId)
        {
            return papersUsersRepository.GetPapersForUser(userId);
        }

        public List<Paper> GetAllPendingPapers()
        {
            return paperRepository.GetAllPendingPapers();
        }

        public void ApprovePaper(Guid? id)
        {
            Paper paperToApprove = GetDetailsForPaper(id).Paper;
            paperRepository.ApprovePaper(paperToApprove);

        }

        public void DenyPaper(Guid? id)
        {
            Paper paperToDeny = GetDetailsForPaper(id).Paper;
            paperRepository.DenyPaper(paperToDeny);
        }

       
    }
}
