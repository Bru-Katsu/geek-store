import { environment } from "src/environments/environment";

export abstract class ApiServiceBase {
  protected apiAddress = environment.apiAddress;
}
