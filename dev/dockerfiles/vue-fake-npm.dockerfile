FROM node:lts AS build-env

WORKDIR /app

RUN pwd

COPY src/app/Foundation.Extension.Admin-UI/package.json /app/lib/Foundation.Extension.Admin-UI/package.json

COPY src/app/Foundation.Extension.Core-UI/package.json /app/lib/Foundation.Extension.Core-UI/package.json

COPY src/shared/Foundation.Extension.Shared-UI/package.json /app/lib/Foundation.Extension.Shared-UI/package.json

COPY dev/helpers/package.json /app/package.json

COPY src/template/src/app/core/XXXXX.Core.UI/package.json src/app/core/XXXXX.Core.UI/package.json
COPY src/template/src/app/admin/XXXXX.Admin.UI/package.json src/app/admin/XXXXX.Admin.UI/package.json

ENV NODE_OPTIONS="--max-old-space-size=8192"

RUN npm install --force

ENTRYPOINT npm run dev -w $PROJECT
# ENTRYPOINT sleep 6000000000000