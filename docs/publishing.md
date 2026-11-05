# Publishing Aster.Net

## One-time setup

1. Merge `publish-release.yml`, its scripts and release configuration into main
   before merging the first preparation PR.
2. In **Settings > Environments**, create `release`. Add yourself as a required
   reviewer, leave **Prevent self-review** unchecked, and restrict deployment
   branches to `main` (`master` for repositories configured that way). Keep the
   separate `version-approval` preparation environment for major/unusual versions.
   The build checks that `release` has required reviewers before offering artifacts.
3. Add Actions repository variable `NUGET_USER`, your NuGet.org profile username,
   not your email address.
4. On NuGet.org, open **Trusted Publishing** and create a policy for owner `JKorf`,
   repository `Aster.Net`, workflow file `publish-release.yml` (filename only),
   environment `release`. Scope it to `Jkorf.Aster.Net`. Publishing uses an OIDC
   exchange for a short-lived key after approval; no NuGet API key secret is needed.
5. For an existing GitHub package, ensure Aster.Net is connected and has Actions
   write access to it. New packages may need their visibility changed to public
   in GitHub's package settings. Publishing uses the workflow's `GITHUB_TOKEN`.
6. Ensure the Manager credential has Actions read/write, Contents read, Pull
   requests read and Deployments write permissions, and belongs to the configured
   required reviewer. See [Manager approval setup](release-automation-setup.md).

References: [NuGet trusted publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing),
[GitHub NuGet registry](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry).

## Each release

Start PrepareAndRelease in Manager, review the preparation PR and merge it in the
browser. The merge starts a run named `Publish release PR #<number>`. It validates
the README notes and project version, then builds, tests and packs the exact merged
commit. Native NUnit warnings appear in the run summary without failing release;
actual test failures prevent reaching approval. Reports and package artifacts are
retained for 30 days. Review the run summary and artifacts before approving in
Manager or GitHub's `release` environment gate.

After approval, the workflow verifies package metadata, commit and SHA256 checksums,
creates the configured Git tag and a draft GitHub release, and attaches both the
NuGet package and symbol package, notes and manifest. It publishes the primary
package to NuGet.org and GitHub Packages, and symbols to NuGet.org. Once the NuGet
package is downloadable with the expected contents, it publishes the GitHub release.
There is no atomic transaction across these services: partial publication can occur.
News API and Discord announcements are not part of this workflow yet.

## Configuration and reuse

`.github/release-config.json` selects `main` or `master` and lists independent
packages. Each preparation PR selects one package using its
`release/<key>/<version>` branch; other packages in that repository are untouched.
Copy the workflow and scripts to other repositories and configure their package
project, README, test projects, package ID and unique tag prefix. README entries
must follow the format produced by Prepare-Release.ps1. Stable three-part versions
are supported. Each repository also needs the setup above.

Optional package settings:

- `releaseIntegrationTests`: defaults to false; set true to run integration tests
  again on the merged commit. This is separate from the preparation run's choice.
- `publishToGitHubPackages`: defaults to true; set false for NuGet.org only.

## Recovering a failed publication

Fix credential/access or service availability problems, then choose **Re-run failed
jobs** on the original run. Approve the new attempt if required. The publish job
downloads the original artifact by immutable artifact ID; it does not rebuild.
Existing tags must resolve to the same commit, release assets must match exactly,
and existing registry packages must have the same ZIP entries (ignoring NuGet's
repository signature). Conflicts stop the run rather than overwrite a release.

Do not choose **Re-run all jobs** after partial publication: a fresh build can
produce different package bytes or metadata, which conflict with existing assets.
Keep the draft, tag and artifact while recovering. If NuGet indexing exceeds five
minutes, publication stops with the draft intact; retry failed jobs later. If the
30-day artifact retention expires, recover from the original draft attachments
manually. Do not delete published versions or move tags to retry different code.
