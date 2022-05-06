using AutoMapper;
using Logitar;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Commands
{
  internal abstract class SaveIssueHandler
  {
    private readonly IIssueRepository _issueRepository;
    private readonly IMapper _mapper;

    protected SaveIssueHandler(IIssueRepository issueRepository, IMapper mapper)
    {
      _issueRepository = issueRepository;
      _mapper = mapper;
    }

    protected async Task<IssueModel> SaveAsync(Issue issue, SaveIssuePayload payload, CancellationToken cancellationToken = default)
    {
      issue.Description = payload.Description?.CleanTrim();
      issue.DueDate = payload.DueDate;
      issue.Estimate = payload.Estimate;
      issue.Name = payload.Name.Trim();
      issue.Score = payload.Score;

      await _issueRepository.SaveAsync(issue, cancellationToken);

      return _mapper.Map<IssueModel>(issue);
    }
  }
}
