import { AppState } from "../AppState"
import { Vault} from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
 async create(newVault) {
  const res = await api.put("/api/vaults", newVault)
  AppState.vaults.unshift(new Vaults(res.data))
  return new Vault(res.data)
 }

 async getAll() {
  const res = await api.get("/api/vaults")
  AppState.vaults = res.data
 }

 async getById(vault) {
  const res = await api.get("/api/vaults" + keep.id)
  AppState.focusVault = res.data
 }

 async getByProfile(creatorId) {
  const res = await api.get("/api/profiles/" + creatorId + "/vaults")
  AppState.vaults = res.data
 }

 async delete(vault) {
  const res = await api.delete("/api/vaults/" + vault.id)
 }
}

export const vaultsService = new VaultsService()