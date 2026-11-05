# Release automation approvals

## Run preparation PR checks automatically

GitHub places workflow runs from PRs created/updated with GITHUB_TOKEN into an
approval-required state. Use a GitHub App installation token to create the release
branch and PR so their normal PR checks can start automatically.

Create one GitHub App for release automation:

1. In your GitHub account's developer settings, create a GitHub App. Webhooks are
   not required for this use case; disable Active for the webhook.
2. Grant repository permissions **Contents: Read and write** and **Pull requests:
   Read and write**. Metadata read access is included automatically.
3. Install the App on Aster.Net and later on the other library repositories.
4. Generate a private key for the App.
5. Under Aster.Net **Settings > Secrets and variables > Actions**, add:
   - Repository variable `RELEASE_APP_ID`: the App ID.
   - Repository secret `RELEASE_APP_PRIVATE_KEY`: the full PEM private key.

The preparation workflow creates a short-lived token scoped to the current
repository immediately before pushing its branch and creating its PR. Both git
push and PR creation use that token. The token action revokes it afterward.
No App credential is supplied to the package build or tests.

If RELEASE_APP_ID is absent, preparation continues using GITHUB_TOKEN and reports
that PR checks may require manual approval. If an App ID is configured but its key
or installation permissions are invalid, preparation fails instead of silently
falling back. Keep normal protections for external contributor PRs in place.

The same App can serve all library repositories. Repository variables/secrets and
installation access must be configured for each repository; this is setup work,
not a per-release action. For personal-account repositories, that setup can be
scripted rather than entered repeatedly through GitHub's website.

## Publishing and batch approval design

The publishing workflow is the next implementation stage; it does not exist yet.
Its trigger will be a preparation PR being merged into the configured main/master
branch. Closing an unmerged PR or merging an ordinary feature PR must not publish.
It will validate the package/release branch and build the exact merged commit,
then pause at a protected `release` environment before any publication.

Keep final publication approval. Manager will provide a review list with each
library, version, source commit, checks/warnings, PR link, and workflow link. An
explicit **Approve selected releases** action will submit the approvals under your
user identity. This is not automatic approval by a bot.

GitHub's deployment-review API is per repository/run, so Manager must make separate
requests for selected releases. One UI action can submit them all, while reporting
individual failures; the operation is not atomic. Before each request, recheck the
run attempt, commit and pending environment to avoid approving stale entries.

The Manager credential must represent a required reviewer for each environment and
have **Deployments: Read and write** and **Actions: Read** access, in addition to
the existing permissions used for preparation. The App token that creates PRs does
not grant approval on your behalf. Leave Prevent self-review unchecked for your
solo-maintainer workflow.

Manager can similarly offer explicit batch merging of selected preparation PRs,
after verifying their required checks. Merges must use your authenticated user
credentials, not GITHUB_TOKEN, so merge events can start the publish workflow.

References:

- https://docs.github.com/en/actions/how-tos/write-workflows/choose-when-workflows-run/trigger-a-workflow
- https://github.com/actions/create-github-app-token
- https://docs.github.com/en/rest/actions/workflow-runs#review-pending-deployments-for-a-workflow-run
