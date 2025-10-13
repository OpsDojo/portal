import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";

import { SpaConfig } from "./spa-config.model";

export function provideSpaConfig(): EnvironmentProviders {
  return makeEnvironmentProviders([
    SpaConfig,
  ]);
}