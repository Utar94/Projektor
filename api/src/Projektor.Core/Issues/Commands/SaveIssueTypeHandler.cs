using AutoMapper;
using Logitar;
using Projektor.Core.Issues.Models;
using Projektor.Core.Issues.Payloads;
using Projektor.Core.Repositories;

namespace Projektor.Core.Issues.Commands
{
  internal abstract class SaveIssueTypeHandler
  {
    private readonly IIssueTypeRepository _issueTypeRepository;
    private readonly IMapper _mapper;

    protected SaveIssueTypeHandler(IIssueTypeRepository issueTypeRepository, IMapper mapper)
    {
      _issueTypeRepository = issueTypeRepository;
      _mapper = mapper;
    }

    protected async Task<IssueTypeModel> SaveAsync(IssueType issueType, SaveIssueTypePayload payload, CancellationToken cancellationToken = default)
    {
      issueType.Description = payload.Description?.CleanTrim();
      issueType.Name = payload.Name.Trim();

      await _issueTypeRepository.SaveAsync(issueType, cancellationToken);

      return _mapper.Map<IssueTypeModel>(issueType);
    }
  }
}
