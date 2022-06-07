import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class KeepsService {

 async create(newKeep) {
  const res = await api.put("/api/keep", newKeep)
  AppState.keeps.unshift(new Keep(res.data))
  return new Keep(res.data)
 }
 async getAll() {
  const res = await api.get("/api/keeps")
  AppState.keeps = res.data
  logger.log(AppState.keeps)
 }

 async getById(keep) {
  const res = await api.get("/api/keeps/" + keep.id)
  AppState.focusKeep = res.data
  logger.log(AppState.focusKeep)
 }

 async delete(keep) {
  const res = await api.delete("/api/keeps/" + keep.id)
 }

 async getByProfile(id) {
  const res = await api.get("/api/profiles/" + id + "/keeps")
  AppState.vaults = res.data
 }
}

export const keepsService = new KeepsService() 