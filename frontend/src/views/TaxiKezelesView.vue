
<template>
    <div>
      <h1>Taxi kezelés</h1>
  
      <!--#region táblázat -->
      <table class="table table-bordered w-auto table-hover">
        <thead>
          <tr>
            <th>
              <!-- post -->
              <button
                type="button btn-sm"
                class="btn btn-outline-success btn-sm"
                @click="onClickNew()"
                data-bs-target="#carModal"
              >
                Új taxi
              </button>
            </th>
            <th>Autó márka</th>
            <th>Rendszám</th>
            <th>Óradíj Ft/óra</th>
            <th>Sofőr</th>
            <th>Forgamon kívül</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(car, index) in carsWithDrivers"
            :key="`car${index}`"
            @click="onClickRow(car.id)"
            :class="currentRowBackground(car.id)"
          >
            <td>
              <!-- törlés delete -->
              <button
                type="button"
                class="btn btn-outline-danger btn-sm"
                @click="onClickDelete(car.id)"
              >
                <i class="bi bi-trash3-fill"></i>
              </button>
              <!-- put -->
              <button
                type="button"
                class="btn btn-outline-primary btn-sm ms-2"
                @click="onClickEdit(car.id)"
                data-bs-target="#carModal"
              >
                <i class="bi bi-pencil-fill"></i>
              </button>
            </td>
            <td>{{ car.name }}</td>
            <td>{{ car.licenceNumber }}</td>
            <td>{{ car.hourlyRate }}</td>
            <td>{{ car.driverName }}</td>
            <td>
              <div class="form-check">
                <input
                  class="form-check-input"
                  type="checkbox"
                  value=""
                  :id="`traffic${index}`"
                  v-model="car.outOfTraffic"
                  disabled
                />
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <!--#endregion táblázat -->
  
      <!--#region Modal  -->
      <div
        class="modal fade"
        id="carModal"
        tabindex="-1"
        aria-labelledby="exampleModalLabel"
        aria-hidden="true"
      >
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h1 class="modal-title fs-5" id="exampleModalLabel">
                {{ stateTitle }}
              </h1>
              <!-- jobb fölső x -->
              <button
                type="button"
                class="btn-close"
                @click="onClickCancel()"
                aria-label="Close"
              ></button>
            </div>
            <!-- Content -->
            <div class="modal-body">
              <form class="row g-3 needs-validation" novalidate>
                <!-- name -->
                <div class="mb-3 col-md-12">
                  <label for="name" class="form-label">Autó neve</label>
                  <input
                    type="text"
                    class="form-control"
                    id="name"
                    required
                    v-model="editableCar.name"
                  />
                  <div class="invalid-feedback">Az autó kitöltése kötelező</div>
                </div>
                <!-- licenceNumber -->
                <div class="mb-3 col-md-6">
                  <label for="licenceNumber" class="form-label">Rendszám</label>
                  <input
                    type="text"
                    class="form-control"
                    id="licenceNumber"
                    required
                    v-model="editableCar.licenceNumber"
                  />
                  <div class="invalid-feedback">
                    Az rendszám kitöltése kötelező
                  </div>
                </div>
                <!-- hourlyRate -->
                <div class="mb-3 col-md-6">
                  <label for="hourlyRate" class="form-label">Óradíj</label>
                  <input
                    type="number"
                    class="form-control"
                    id="hourlyRate"
                    required
                    v-model="editableCar.hourlyRate"
                  />
                  <div class="invalid-feedback">Az óradíj kitöltése kötelező</div>
                </div>
                <div class="d-flex align-items-center">
                  <!-- outOfTraffic -->
                  <div class="col-4">
                    <div class="form-check">
                      <input
                        class="form-check-input"
                        type="checkbox"
                        value=""
                        id="outOfTraffic"
                        v-model="editableCar.outOfTraffic"
                      />
                      <label class="form-check-label" for="outOfTraffic">
                        Forgalmon kívül
                      </label>
                    </div>
                  </div>
                  <!-- drivers  -->
                  <div class="col-md-8 d-flex align-items-center">
                    <label for="driverId" class="form-label m-0">Sofőr:</label>
                    <select
                      class="form-select ms-2"
                      id="driverId"
                      v-model="editableCar.driverId"
                      required
                    >
                      <option :value="null">Nincs sofőr</option>
                      <option
                        v-for="(driver, index) in driversAbc"
                        :key="`driver${index}`"
                        :value="driver.id"
                      >
                        {{ driver.driverName }}
                      </option>
                    </select>
                  </div>
                </div>
              </form>
            </div>
  
            <div class="modal-footer">
              <button
                type="button"
                class="btn btn-secondary"
                @click="onClickCancel()"
              >
                Close
              </button>
              <button
                type="button"
                class="btn btn-primary"
                @click="onClickSave()"
              >
                Mentés
              </button>
            </div>
          </div>
        </div>
      </div>
      <!--#endregion Modal  -->
    </div>
  </template>
  
  <script>
  import * as bootstrap from "bootstrap";
  import { useUrlStore } from "@/stores/url";
  import { useLoginStore } from "@/stores/login";
  const storeUrl = useUrlStore();
  const storeLogin = useLoginStore();
  
  class Car {
    constructor(
      id = 0,
      name = null,
      licenceNumber = null,
      hourlyRate = null,
      outOfTraffic = false,
      driverId = null
    ) {
      this.id = id;
      this.name = name;
      this.licenceNumber = licenceNumber;
      this.hourlyRate = hourlyRate;
      this.outOfTraffic = outOfTraffic;
      this.driverId = driverId;
    }
  }
  export default {
    data() {
      return {
        storeUrl,
        storeLogin,
        carsWithDrivers: [],
        editableCar: new Car(),
        carModal: null,
        form: null,
        state: "view",
        currentId: null,
        driversAbc: [],
      };
    },
    mounted() {
      this.getCarsWithDrivers();
      this.getDriversAbc();
      this.carModal = new bootstrap.Modal(document.getElementById("carModal"), {
        keyboard: false,
      });
      
      //A validációhoz kell
      this.form = document.querySelector(".needs-validation");
  
  
    },
    methods: {
      async getCarsWithDrivers() {
        let url = this.storeUrl.urlCarsWithDrivers;
        const config = {
          method: "GET",
          headers: {
            Authorization: `Bearer ${this.storeLogin.accessToken}`,
          },
        };
        const response = await fetch(url, config);
        const data = await response.json();
        this.carsWithDrivers = data.data;
        this.state = "view";
        this.getDriversAbc();
        
      },
      async getCarById(id) {
        let url = `${this.storeUrl.urlCars}/${id}`;
        const config = {
          method: "GET",
          headers: {
            Authorization: `Bearer ${this.storeLogin.accessToken}`,
          },
        };
        const response = await fetch(url, config);
        const data = await response.json();
        this.editableCar = data.data;
      },
  
      async getDriversAbc() {
        let url = this.storeUrl.urlDriversFreAbc;
        const config = {
          method: "GET",
          headers: {
            Authorization: `Bearer ${this.storeLogin.accessToken}`,
          },
        };
        const response = await fetch(url, config);
        const data = await response.json();
        this.driversAbc = data.data;
      },
      async postCar() {
        let url = this.storeUrl.urlCars;
        const body = JSON.stringify(this.editableCar);
        const config = {
          method: "POST",
          headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${this.storeLogin.accessToken}`,
          },
          body: body,
        };
        const response = await fetch(url, config);
        this.getCarsWithDrivers();
      },
      async putCar() {
        const id = this.editableCar.id;
        let url = `${this.storeUrl.urlCars}/${id}`;
        const body = JSON.stringify(this.editableCar);
        const config = {
          method: "PUT",
          headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${this.storeLogin.accessToken}`,
          },
          body: body,
        };
        const response = await fetch(url, config);
        this.getCarsWithDrivers();
      },
      async deleteCar(id) {
        let url = `${this.storeUrl.urlCars}/${id}`;
        const config = {
          method: "DELETE",
          headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${this.storeLogin.accessToken}`,
          },
        };
        const response = await fetch(url, config);
        this.getCarsWithDrivers();
      },
  
  
      onClickNew() {
        this.state = "new";
        this.editableCar = new Car();
        this.getDriversAbc();
        this.carModal.show();
      },
      onClickEdit(id) {
        this.state = "edit";
        this.getCarById(id);
        this.carModal.show();
      },
      onClickDelete(id) {
        this.state = "delete";
        this.deleteCar(id);
      },
      onClickCancel() {
        this.editableCar = new Car();
        this.carModal.hide();
      },
      onClickSave() {
        this.form.classList.add("was-validated");
        if (this.form.checkValidity()) {
          if (this.state == "new") {
            this.postCar();
          } else if (this.state == "edit") {
            this.putCar();
          }
          this.carModal.hide();
        }
        
      },
      onClickRow(id) {
        this.currentId = id;
      },
      currentRowBackground(id) {
        return this.currentId == id ? "my-bg-current-row" : "";
      },
    },
    computed: {
      stateTitle() {
        if (this.state === "new") {
          return "Új autó bevitele";
        } else if (this.state === "edit") {
          return "Autó módosítás";
        }
      },
    },
  };
  </script>
  
  
  <style>
  .my-bg-current-row {
    background-color: lightgray;
  }
  </style>