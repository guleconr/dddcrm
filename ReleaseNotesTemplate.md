**Release Notes**

[[_TOC_]]

# Release Notes

|         | RELEASE | BUILD |
|---------|---------|-------|
| Definition Name | ${releaseDetails.releaseDefinition.name} | ${releaseDetails.artifacts[0].definitionReference.definition.name} |
| Definition ID | ${releaseDetails.releaseDefinition.id} | ${releaseDetails.artifacts[0].definitionReference.definition.id} |
| Current Name | ${releaseDetails.name} |  ${releaseDetails.artifacts[0].definitionReference.version.name} |
| URL | ${releaseDetails._links.web.href} | ${releaseDetails.artifacts[0].definitionReference.artifactSourceVersionUrl.id} |
| Completed | ${releaseDetails.modifiedOn} | N/A |
| Compared with | ${compareReleaseDetails.name} | N/A |
| Source Branch | N/A | ${releaseDetails.artifacts[0].definitionReference.branch.name} |

## Technical Description
@@WILOOP:Release Notes:RN-TECH-SUMMARY@@
${widetail.fields['System.Description']}
@@WILOOP:Release Notes:RN-TECH-SUMMARY@@

## Business Description
@@WILOOP:Release Notes:RN-BUSINESS-BLURB@@
${widetail.fields['System.Description']}
@@WILOOP:Release Notes:RN-BUSINESS-BLURB@@

## Dependencies
@@WILOOP:Release Notes:RN-DEPENDENIES@@
${widetail.fields['System.Description']}
@@WILOOP:Release Notes:RN-DEPENDENIES@@

## Known Issues
@@WILOOP:Release Notes:RN-KNOWN-ISSUES@@
${widetail.fields['System.Description']}
@@WILOOP:Release Notes:RN-KNOWN-ISSUES@@

## Technical Debt
@@WILOOP:Release Notes:RN-TECH-DEBT@@
${widetail.fields['System.Description']}
@@WILOOP:Release Notes:RN-TECH-DEBT@@

## Fallback Plan
@@WILOOP:Release Notes:RN-FALLBACK-PLAN@@
${widetail.fields['System.Description']}
@@WILOOP:Release Notes:RN-FALLBACK-PLAN@@

---

## Associated Artifacts

### Associated work items
@@WILOOP@@
* #${widetail.id}
@@WILOOP@@

### Associated commits
@@CSLOOP@@
* **ID ${csdetail.id} ** ${csdetail.message}
@@CSLOOP@@