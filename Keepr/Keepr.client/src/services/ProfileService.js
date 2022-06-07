import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class ProfilesService {

 async getById(id) {
  const res = await api.get("/api/profiles/" + id)
  AppState.profile = res.data
  logger.log(AppState.profile)
 }

}

export const profilesService = new ProfilesService() 