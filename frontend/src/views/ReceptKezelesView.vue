
<template>
  <div>
    <h1>Recept kezelés</h1>
    <hr />
    <div class="row p-3">
      <!-- taxi táblázat -->
      <div class="col-md-5">
        <h2>Receptek</h2>
        <button
          type="button"
          class="btn btn-primary ms-5 mb-2"
          title="Új étel hozzáadása"
          @click="onClickNewFood()"
        >
          <i class="bi bi-plus-lg"></i>
        </button>

        <!--#region táblázat -->
        <table class="table table-bordered table-hover w-auto ms-0">
          <thead>
            <tr>
              <th>Esesmények</th>
              <th>Étel neve</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(food, index) in foodWithCategrory" :key="`car${index}`">
              <td>
                <button
                  type="button"
                  class="btn btn-danger me-2"
                  title="Étel törlése"
                  @click="onClickDelete(food.id)"
                >
                  <i class="bi bi-trash"></i>
                </button>
                <button
                  type="button"
                  class="btn btn-success"
                  title="Étel módosítása"
                  @click="onClickShowIngredient()"
                >
                  <i class="bi bi-gear"></i>
                </button>
              </td>
              <td>{{ food.foodName }}</td>
            </tr>
          </tbody>
        </table>
        <!--#endregion táblázat -->
      </div>

      <!--#region Modal  -->
      <div
        class="modal fade"
        id="ActionFoodModal"
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
                  <label for="foodName" class="form-label">Étel neve</label>
                  <input
                    type="text"
                    class="form-control"
                    id="foodName"
                    required
                    v-model="editableFood.foodName"
                  />
                  <div class="invalid-feedback">Az autó kitöltése kötelező</div>
                </div>
                <!-- licenceNumber -->
                <div class="mb-3 col-md-6">
                  <label for="categoryID" class="form-label">Kategória</label>

                  <select
                  class="form-select" 
                  v-model="editableFood.categoryName"
                  >
                  <option 
                  v-for="(category, index) in category"
                  :key="`category${index}`"
                  >
                  {{category.categoryName}}

                  </option>


                  </select>

                  <!-- <input
                    type="text"
                    class="form-control"
                    id="categoryID"
                    required
                    v-model="editableFood.categoryName"
                  /> -->
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
                    v-model="editableFood.descriptionDate"
                  />
                  <div class="invalid-feedback">
                    Az óradíj kitöltése kötelező
                  </div>
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
                        v-model="editableFood.firstDate"
                      />
                      <label class="form-check-label" for="outOfTraffic">
                        Forgalmon kívül
                      </label>
                    </div>
                  </div>
                  <!-- drivers  -->
                  <!-- <div class="col-md-8 d-flex align-items-center">
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
                  </div> -->
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

      <!-- taxi fuvarjai -->
      <!-- <div class="col-md-7">
        <h2>Taxi fuvarkezelése</h2>
        <hr />
        <div>
          <h3>Új fuvar</h3>
          <form class="row g-3 ">
            A fuvar dátumideje
            <div class="col-md-6 d-flex align-items-center">
              <label for="date" class="form-label">Mikor</label>
              <input
                type="datetime-local"
                class="form-control ms-2"
                id="date"
                required
                v-model="newTrip.date"
                ref="date"
              />
              <div class="invalid-feedback">A dátum kitöltése kötelező</div>
            </div>
            Menetidő (perc)
            <div class="col-md-6 d-flex align-items-center">
              <label for="numberOfMinits" class="form-label">Menetidő:</label>
              <input
                type="number"
                class="form-control ms-2"
                id="numberOfMinits"
                required
                v-model="newTrip.numberOfMinits"
                ref="numberOfMinits"
              />
              <div class="invalid-feedback">A menetidő kitöltése kötelező</div>
              <button
                type="button"
                class="btn btn-outline-success ms-2"
                @click="onClickSave()"
                ref="save"
                @keyup.enter="onClickSave()"
              >
                <i class="bi bi-save2"></i>
              </button>
            </div>
          </form>
          <hr />

          <h3>Eddigi fuvarok</h3>
          <ul>
            <li v-for="(trip, index) in tripsByCarId" :key="`trip${index}`">
              {{ trip.date }}: {{ trip.numberOfMinits }} perc
              <span
                class="ms-2 my-delete-hover"
                @click="onClickDeleteTrip(trip.id)"
                ><i class="bi bi-trash3-fill"></i
              ></span>
            </li>
          </ul>
        </div>
      </div>-->
    </div>

    <!-- delete modal component -->
    <Menu></Menu>
    <YesNo
      v-if="yesNoShow"
      yesNoTitle="Fuvar törlés"
      yesNoMessage="Valóban törölni akarja a fuvart?"
      @yes="onClickDeleteOK()"
      @no="onClickDeleteCancel()"
    ></YesNo>
  </div>
</template>

<script>
import * as bootstrap from "bootstrap";
import { useUrlStore } from "@/stores/url";
import { useLoginStore } from "@/stores/login";
import YesNo from "../components/YesNo.vue";
import Menu from "../components/Menu.vue";
const storeUrl = useUrlStore();
const storeLogin = useLoginStore();

class Food {
  constructor(
    id = 0,
    foodName = null,
    categoryID = 0,
    descriptionDate = null,
    firstDate = null,
    categoryName = null,
    ingredientName = null,
    quantity = null,
    unit = null
  ) {
    (this.id = id),
      (this.foodName = foodName),
      (this.categoryID = categoryID),
      (this.descriptionDate = descriptionDate),
      (this.firstDate = firstDate),
      (this.categoryName = categoryName),
      (this.ingredientName = ingredientName),
      (this.quantity = quantity),
      (this.unit = unit);
  }
}

export default {
  data() {
    return {
      storeUrl,
      storeLogin,
      cars: [],
      tripsByCarId: [],
      yesNoShow: false,
      foodWithCategrory: [],
      state: "view",
      currentId: null,
      editableFood: new Food(),
      modal: null,
      category: null,
      form: null,
    };
  },
  mounted() {
    this.getfoodWithCategrory();
    // this.getCarsWithDriversReal();
    this.modal = new bootstrap.Modal(document.getElementById("foodModal"), {
      keyboard: false,
    });
    this.form = document.querySelector(".needs-validation");
  },
  methods: {
    async getfoodWithCategrory() {
      let url = this.storeUrl.urlfoodWithCategrory;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.foodWithCategrory = data.data;
    },

    async getCarsWithDriversReal() {
      let url = this.storeUrl.urlCarsWithDriversReal;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.cars = data.data;
    },
    async getfoodWithEveritingById() {
      let url = `${this.storeUrl.urlTripsByCarId}/${this.currentCarId}`;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.tripsByCarId = data.data;
      this.newTrip = new Food();
    },
    async getCategory() {
      let url = `${this.storeUrl.urlCategory}`;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.category = data.data;
    },
    async postTrip() {
      let url = this.storeUrl.urlTrips;
      this.newTrip.carId = this.currentCarId;
      const body = JSON.stringify(this.newTrip);
      const config = {
        method: "POST",
        headers: {
          "content-type": "application/json",
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
        body: body,
      };
      const response = await fetch(url, config);
      this.getTripsByCarId();
    },
    async deleteTrip(id) {
      let url = `${this.storeUrl.urlTrips}/${id}`;
      this.newTrip.carId = this.currentCarId;
      const body = JSON.stringify(this.newTrip);
      const config = {
        method: "DELETE",
        headers: {
          "content-type": "application/json",
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
        body: body,
      };
      const response = await fetch(url, config);
      this.getfoodWithEveritingById();
    },
    async getfoodWithEverithingById(id) {
      let url = `${this.storeUrl.urlfoodWithEverithingById}/${id}`;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.foodWithEverithingById = data.data;
    },
    currentRowBackground(id) {
      return this.currentId == id ? "my-bg-current-row" : "";
    },
    onClikRow(id) {
      this.currentId = id;
      this.getfoodWithEveritingById(id);
      // this.$refs.date.focus();
      // this.$refs.date.showPicker();
    },
    onClickSave() {
      this.postTrip();
    },
    onClickDeleteTrip(id) {
      this.yesNoShow = true;
      this.currentTripId = id;
    },
    onClickDeleteOK() {
      this.deleteTrip(this.currentTripId);
      this.yesNoShow = false;
    },
    onClickDeleteCancel() {
      this.yesNoShow = false;
    },
    onClickNewFood() {
      this.getCategory();
      this.modal.show();
      this.state = "new";
      this.currentId = null;
      this.editableFood = new Food();
    },
    onClickCancel(){
      this.modal.hide();

    },
  },
  components: { YesNo, Menu },

   computed: {
      stateTitle() {
        if (this.state === "new") {
          return "Új étel bevitele";
        } else if (this.state === "edit") {
          return "Étel módosítás";
        }
      },
    },
};
</script>


<style>
.my-bg-current-row {
  background-color: lightgrey;
}
tr:hover {
  cursor: pointer;
}
.my-delete-hover:hover {
  color: red;
}
</style>
