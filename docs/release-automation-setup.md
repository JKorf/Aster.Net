# Release automation approvals

## Run preparation PR checks automatically

GitHub places workflow runs from PRs created/updated with GITHUB_TOKEN into an
approval-required state. Use a GitHub App installation token to create the release
branch and PR so their normal PR checks can start automatically.

Create one GitHub App for release automation:

1. In your GitHub account's developer settings, create a GitHub App. Webhooks are
   not required for this use case; disable Active for the webhook.
2. Grant repository permissions **Contents: Read and write** and **Pull requests:
   Read and write**, plus **Workflows: Read and write** for publication/recovery.
   Metadata read access is included automatically. For an existing App, save the
   changed permissions and accept the permission update on its installation.
3. Install the App on Aster.Net and later on the other library repositories.
4. Generate a private key for the App.
5. Under Aster.Net **Settings > Secrets and variables > Actions**, add:
   - Repository variable `RELEASE_APP_ID`: the App ID.
   - Repository secret `RELEASE_APP_PRIVATE_KEY`: the full PEM private key.

The preparation workflow creates a short-lived token scoped to the current
repository immediately before pushing its branch and creating its PR. Both git
push and PR creation use that token. The token action revokes it afterward.
No App credential is supplied to the package build or tests.
After publication approval, the publishing job creates a separate App token with
Contents/Workflows write access for tags and releases. GitHub can require workflow
permission when tagging an older commit whose workflow files differ from main;
GITHUB_TOKEN cannot supply it. GitHub Packages continues using GITHUB_TOKEN.

If RELEASE_APP_ID is absent, preparation continues using GITHUB_TOKEN and reports
that PR checks may require manual approval. If an App ID is configured but its key
or installation permissions are invalid, preparation fails instead of silently
falling back. Keep normal protections for external contributor PRs in place.

The same App can serve all library repositories. Repository variables/secrets and
installation access must be configured for each repository; this is setup work,
not a per-release action. For personal-account repositories, that setup can be
scripted rather than entered repeatedly through GitHub's website.

## Publishing and per-library approval

The `publish-release.yml` workflow starts when a preparation PR is merged into
the configured main/master branch. Closed, unmerged PRs and ordinary feature PRs
do not publish. It validates the package/release branch, builds/tests/packs the
exact merged commit, and pauses at the protected `release` environment before
any publication. Complete the [publishing setup](publishing.md) before the first
release, including the required reviewer and NuGet trusted publishing policy.

Keep final publication approval and one Manager job window per library.
Library.PrepareAndRelease calls Library.PrepareRelease, then Library.ApproveRelease.
You open the preparation PR in your browser, review it and merge it. The second
job waits for that merge and the resulting publishing workflow, then asks for your
explicit approval in Manager. It rechecks the run attempt, commit and environment
before submitting that approval, then monitors publication to completion.

The confirmation shows the package, version, merged commit, PR and publishing run.
Use the workflow link to review final tests, warnings and artifacts before selecting
Yes. No leaves the workflow waiting. The job does not merge PRs or approve a release
automatically. Library.ApproveRelease can also resume an existing PR after a timeout
or Manager restart without starting preparation again.

The Manager credential must represent a required reviewer for each environment and
have **Deployments: Read and write** and **Actions: Read** access, in addition to
the existing permissions used for preparation. The App token that creates PRs does
not grant approval on your behalf. Leave Prevent self-review unchecked for your
solo-maintainer workflow.

Merge the PR in the browser with your user account so the merge event starts the
publishing workflow. That workflow uses the run name
`Publish release PR #<number>` to let Manager identify the matching publication.
Install the workflow and its scripts in each library repository before enabling
publication there. Package selection and main/master support come from the
repository's `.github/release-config.json`.

References:

- https://docs.github.com/en/actions/how-tos/write-workflows/choose-when-workflows-run/trigger-a-workflow
- https://github.com/actions/create-github-app-token
- https://docs.github.com/en/rest/actions/workflow-runs#review-pending-deployments-for-a-workflow-run
