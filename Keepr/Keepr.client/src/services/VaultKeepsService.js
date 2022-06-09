import { AppState } from "../AppState"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class VaultKeepsService {

  async createVk(vaultKeep) {
  vaultKeep.keepId = AppState.focusKeep.id;
  const res = await api.post("/api/vaultkeeps", vaultKeep)
  logger.log(res.data)
 }



 async getById(keep) {
  const res = await api.get("/api/keeps/" + keep.id)
  AppState.focusKeep = res.data
  logger.log(AppState.focusKeep)
 }

 async delete(keep) {
  const res = await api.delete("/api/vaultkeeps/" + keep.id)
 }

 async getByProfile(id) {
  const res = await api.get("/api/profiles/" + id + "/keeps")
  logger.log(res.data)
  AppState.keeps = res.data
 }

  async getByVault(id) {
  const res = await api.get("api/vaultkeeps/" + id + "/keeps")
  logger.log("Howdy", res.data)
  AppState.keeps = res.data
  }
 
 
}

export const vaultKeepsService = new VaultKeepsService() 