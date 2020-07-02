# thebookindex
thebookindex.net




Angular App Setup and deployment to AWS S3 Bucket

Initial Setup
=============
(Based on this post: https://medium.com/@ibliskavka/aws-angular-stack-automation-b45767bda2ec)
1) The CF Template will not deploy / update the CF stack in AWS as part of the CI/CD pipeline

Website and API Updates
=======================
1) Push or Merge to the Master branch of the TheBookIndex repo. CI/CD will take care of it
(Also based on the above post, but running in a Github Action)

Note:
- If the build for either the Website or the API fails, neither will be updated.
- The Api will deploy first. If this fails, the Website will not be deployed.

