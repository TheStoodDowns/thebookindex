# thebookindex
thebookindex.net




Angular App Setup and deployment to AWS S3 Bucket

Initial Setup
=============
(Based on this post: https://medium.com/@ibliskavka/aws-angular-stack-automation-b45767bda2ec)
1) Ensure AWS CLI is installed and IAM Credentials are configured
2) run the following:
    aws cloudformation deploy --template-file angular-app-stack.yaml --stack-name angular-app-stack
3) Update the "deploy" script in the package.json of the angular app (TheBookIndex.Web) to use the bucket name and distribution id from the stack output.

Website Updates
===============
1) Push or Merge to the Master branch of the TheBookIndex repo. CI/CD will take care of it
(Also based on the above post, but running in a Github Action)

