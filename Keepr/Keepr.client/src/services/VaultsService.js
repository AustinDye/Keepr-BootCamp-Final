import { AppState } from "../AppState"
import { Vault} from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
 async createVault(newVault) {
  const res = await api.post("api/vaults", newVault)
  AppState.vaults.unshift(new Vault(res.data))
  return new Vault(res.data)
 }

 async getAll() {
  const res = await api.get("api/vaults")
  AppState.vaults = res.data
 }

 async getById(vault) {
  const res = await api.get("api/vaults/" + vault.id)
  AppState.focusVault = res.data
 }

 async getByProfile(creatorId) {
  const res = await api.get("api/profiles/" + creatorId + "/vaults")
  logger.log(res.data)
  AppState.vaults = res.data
 }

 async delete(vault) {
  const res = await api.delete("/api/vaults/" + vault.id)
 }
}

export const vaultsService = new VaultsService()