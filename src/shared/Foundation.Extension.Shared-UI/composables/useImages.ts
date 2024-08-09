import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui/core";
import type { ImageDetailsDTO } from "@dative-gpi/foundation-shared-domain/models";
import { ImageDetails } from "@dative-gpi/foundation-shared-domain/models";

import { IMAGE_URL } from "../config/urls";

const ImageServiceFactory = new ServiceFactory<ImageDetailsDTO, ImageDetails>("image", ImageDetails).create(factory => factory.build(
    factory.addGet(IMAGE_URL),
    factory.addNotify()
));

export const useImage = ComposableFactory.get(ImageServiceFactory);