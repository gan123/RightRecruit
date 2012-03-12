using System;
using System.Collections.Generic;
using RightRecruit.Domain.User;

namespace RightRecruit.Domain.Client
{
    public class Position : Entity
    {
        public DenormalizedReference<Client> Client { get; set; }
        public PositionLevel Level { get; set; }
        public PositionStatus Status { get; set; }
        public List<DenormalizedReference<CandidateStatus>> CandidateStatuses { get; set; }
    }

    public class CandidateStatus : Entity
    {
        public DenormalizedReference<Position> PositionAppliedFor { get; set; }
        public DenormalizedReference<Candidate> Candidate { get; set; }
        public DenormalizedReference<InterviewStatus> InterviewStatus { get; set; }
        public DenormalizedReference<Recruiter> SentBy { get; set; }
        public DateTime StatusAsOf { get; set; }
        public List<DenormalizedReference<CommentsHistory>> Comments { get; set; }

    }

    public class CommentsHistory : Entity
    {
        public DenormalizedReference<CandidateStatus> CandidateStatus { get; set; }
        public string Comment { get; set; }
        public DenormalizedReference<User.User> User { get; set; }
    }

    public class InterviewStatus : Entity
    {
        
    }
}