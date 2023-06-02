
<template>
  <div>
    <div class="d-flex align-items-center p-3">
      <h1>Receptek</h1>
      <!-- kereso -->
      <form class="d-flex" role="search" id="kereso">
        <input
          class="form-control me-2"
          type="search"
          placeholder="Étel keresése"
          aria-label="Search"
          v-model="keresoSzo"
        />
        <button
          class="btn btn-outline-success"
          @click="onClickSearch()"
          type="button"
        >
          Keresés
        </button>
      </form>
      <!-- dropdown -->

      <div class="ms-3">
        <strong>Kategória</strong>: {{ categoryNameTitle }}
      </div>

      <div class="btn-group" id="kategoriak">
        <button
          class="btn btn-dark btn-lg dropdown-toggle"
          type="button"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Kategóriák
        </button>
        <ul class="dropdown-menu">
          <li>
            <a
              class="dropdown-item"
              id="0"
              @click="getfoodWithCategrory()"
              href="#"
              >Összes</a
            >
          </li>
          <li v-for="(category, index) in category" :key="`category${index}`">
            <a
              class="dropdown-item"
              href="#"
              @click="onClickFilterCategory(category.id, category.categoryName)"
              >{{ category.categoryName }}</a
            >
          </li>
        </ul>
      </div>
    </div>

    <div class="row row-cols-1 row-cols-md-3 g-4 my-cards">
      <div
        class="col"
        v-for="(food, index) in foodWithCategrory"
        :key="`food${index}`"
      >
        <div class="card">
          <img
            :src="'../../public/' + food.foodName + '.jpg'"
            class="card-img-top my-img"
            :alt="`${food.foodName}`"
            :title="`${food.foodName}`"
          />
          <!-- <img :onload="getImgUrl(food.foodName)"> -->

          <div class="card-body">
            <h5 class="card-title bigLEtter">{{ food.foodName }}</h5>
            <p class="card-text">
              <strong>Az étel feljegyzésének a dátuma:</strong>
              {{ food.descriptionDate }} <br />
              <strong>Az étel első készítésének dátuma:</strong>
              {{ food.firstDate }} <br />
              <strong>Kategória: </strong>
              <span>
                {{ food.categoryName }}
              </span>
            </p>
            <div class="d-flex align-items-center">
              <button
                @click="onClickShowIngredient(food.id)"
                class="btn btn-primary"
              >
                Alapanyagok
              </button>

              <button
                type="button"
                class="btn btn-danger ms-5"
                title="Étel törlése"
                @click="onClickDeleteFood(food.id)"
                v-if="storeLogin.loginSuccess"
              >
                <i class="bi bi-trash"></i>
              </button>
              <button
                type="button"
                class="btn btn-success ms-3"
                title="Étel módosítása"
                @click="onClickUpdateFood(food.id)"
                v-if="storeLogin.loginSuccess"
              >
                <i class="bi bi-gear"></i>
              </button>
            </div>
          </div>
        </div>
      </div>

      <!-- <div class="my-up">
        <button>Fel</button>
      </div> -->
    </div>

    <!--#region Modal -->
    <div
      class="modal fade"
      id="modalFood"
      tabindex="-1"
      aria-labelledby="modalFoodModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1
              class="modal-title fs-5 bigLEtter"
              id="modalFoodModalLabel"
              v-for="(food, index) in foodWithCategroryById"
              :key="`food${index}`"
            >
              {{ food.foodName }}
            </h1>
            <button
              type="button"
              class="btn-close"
              @click="onClickCancel()"
              aria-label="Close"
            ></button>
          </div>

          <!--#region Modal body -->
          <div class="modal-body">
            <table class="table table-bordered table-hover w-auto">
              <thead>
                <tr>
                  <th></th>
                  <th>Alapanyag</th>
                  <th>Mennyiség</th>
                  <th>Egység</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="(food, index) in foodWithEverithingById"
                  :key="`food${index}`"
                >
                  <td>
                    <button
                      type="button"
                      class="btn btn-danger"
                      title="Hozzávaló törlése"
                      @click="onClickDeleteIngredient(food.id)"
                    >
                      <i class="bi bi-trash"></i>
                    </button>

                    <button
                      type="button"
                      class="btn btn-success ms-3"
                      title="Hozzávaló módosítása"
                      @click="onClickUpdateIngredient(food.id)"
                    >
                      <i class="bi bi-gear"></i>
                    </button>
                  </td>
                  <td>{{ food.ingredientName }}</td>
                  <td>{{ food.quantity }}</td>
                  <td>{{ food.unit }}</td>
                </tr>
              </tbody>
            </table>
            <!-- v-if="storeLogin.loginSuccess" -->
            <div>
              <button
                @click="onClicNewIngredient()"
                type="button"
                class="btn btn-primary"
              >
                Új alapanyag hozzáadása
              </button>
            </div>
          </div>
          <!--#endregion Modal body -->

          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              @click="onClickCancel()"
            >
              Close
            </button>
          </div>
        </div>
      </div>
    </div>
    <!--#endregion Modal -->

    <!-- #region modalIngredient -->

    <div
      class="modal fade"
      id="modalIngredient"
      tabindex="-1"
      aria-labelledby="exampleModalIngredient"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalIngredient">
              {{ stateTitleIngredient }}
            </h1>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">
            <div class="row">
              <select
                class="form-select col ms-4"
                aria-label="Default select example"
                v-model="usedRow.ingredientID"
              >
                <option
                  v-for="(ingredient, index) in ingredients"
                  :key="`ingredient${index}`"
                  :value="ingredient.id"
                >
                  {{ ingredient.ingredientName }}
                </option>
              </select>

              <div class="col">
                <input
                  type="number"
                  class="form-control"
                  placeholder="Mennyiség"
                  aria-label="Mennyiség"
                  aria-describedby="basic-addon1"
                  v-model="usedRow.quantity"
                />
              </div>

              <select
                class="form-select col me-5 pe-3"
                aria-label="Default select example"
                v-model="usedRow.unit"
              >
                <option
                  v-for="(unit, index) in units"
                  :key="`unit${index}`"
                  :value="unit.unit"
                >
                  {{ unit.unit }}
                </option>
              </select>
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Close
            </button>
            <button
              type="button"
              class="btn btn-primary"
              @click="onClickSaveIngredient()"
            >
              Mentés
            </button>
          </div>
        </div>
      </div>
    </div>
    <!--#endregion modalIngredient -->

    <!-- Button trigger modal -->
    <button
      type="button"
      class="btn btn-primary"
      data-bs-toggle="modal"
      data-bs-target="#exampleModal"
    >
      Launch demo modal
    </button>

    <!-- #region Modal -->
    <div
      class="modal fade"
      id="modal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <div class="modal-body">...</div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="modal"
            >
              Close
            </button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div>
        </div>
      </div>
    </div>
    <!-- #endregion Modal -->

  </div>
</template>

<script>
import * as bootstrap from "bootstrap";
import { storeToRefs } from "pinia";
import { useKeresStore } from "@/stores/keres";
import { useUrlStore } from "@/stores/url";
import { useLoginStore } from "@/stores/login";
const storeUrl = useUrlStore();
const storeLogin = useLoginStore();
const storeKeres = useKeresStore();
const { keresoSzo } = storeToRefs(storeKeres);

class Used {
  constructor(
    id = 0,
    quantity = null,
    unit = null,
    foodID = null,
    ingredientID = null
  ) {
    (this.id = id),
      (this.quantity = quantity),
      (this.unit = unit),
      (this.foodID = foodID),
      (this.ingredientID = ingredientID);
  }
}

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
      foodWithCategrory: [],
      foodWithCategroryById: [],
      foodWithEverithingById: [],
      form: null,
      modal: null,
      currentId: null,
      category: [],
      categoryWithFood: [],
      keresoSzo: null,
      foodWithCategroryFilter: [],
      urlfoodWithCategroryBySearch: [],
      categoryNameTitle: "Összes",
      editableFood: new Food(),
      state: "view",
      currentId: null,
      used: [],
      ingredients: [],
      units: [],
      usedRow: new Used(),
      yesNoShow: false,
    };
  },
  mounted() {
    this.getfoodWithCategrory();
    this.getCategory();
    this.modalFood = new bootstrap.Modal(document.getElementById("modalFood"), {
      keyboard: false,
    });
    this.modalIngredient = new bootstrap.Modal(
      document.getElementById("modalIngredient"),
      {
        keyboard: false,
      }
    );
    this.form = document.querySelector(".needs-validation");
    this.getUnits();
    this.getIngredient();
  },
  watch: {
    // keresoSzo(){
    //   if (this.keresoSzo.trim()) {
    //     this.getfoodWithCategroryBySearch();
    //   } else {
    //     this.getfoodWithCategrory();
    //   }
    // }
  },
  methods: {
    async getfoodWithCategrory() {
      this.categoryNameTitle = "Összes";
      this.keresoSzo = null;
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
    async getfoodWithCategroryById(id) {
      let url = `${this.storeUrl.urlfoodWithCategroryById}/${id}`;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.foodWithCategroryById = data.data;
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

    async getCategoryFilter(id, categoryName) {
      const url = `${this.storeUrl.urlfoodSearchByCategory}/${categoryName}`;
      const response = await fetch(url);
      const data = await response.json();
      this.foodWithCategrory = data.data;
    },
    async getfoodWithCategroryBySearch() {
      const url = `${this.storeUrl.urlfoodWithCategroryBySearch}/${this.keresoSzo}`;
      const response = await fetch(url);
      const data = await response.json();
      this.foodWithCategrory = data.data;
    },
    async getUsed() {
      let url = `${this.storeUrl.urlUsed}`;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.used = data.data;
    },
    async getUnits() {
      let url = `${this.storeUrl.urlUnits}`;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.units = data.data;
    },

    async getIngredient() {
      let url = `${this.storeUrl.urlIngredient}`;
      const config = {
        method: "GET",
        headers: {
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      const data = await response.json();
      this.ingredients = data.data;
    },

    async deleteFood(id) {
      let url = `${this.storeUrl.urlfoodWithCategroryById}/${id}`;
      const config = {
        method: "DELETE",
        headers: {
          "content-type": "application/json",
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      this.getfoodWithCategrory();
    },
    async postUsedRow() {
      let url = this.storeUrl.urlUsed;
      const body = JSON.stringify(this.usedRow);
      const config = {
        method: "POST",
        headers: {
          "content-type": "application/json",
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
        body: body,
      };
      const response = await fetch(url, config);
      this.getfoodWithEverithingById(this.usedRow.foodID);
      this.usedRow.ingredientID = null;
      this.usedRow.unit = null;
      this.usedRow.quantity = null;
    },
    async deleteUsed(id) {
      let url = `${this.storeUrl.urlUsed}/${id}`;
      const config = {
        method: "DELETE",
        headers: {
          "content-type": "application/json",
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
      };
      const response = await fetch(url, config);
      this.getfoodWithEverithingById(this.usedRow.foodID);
    },
    async putUsedRow(currentId) {
      const id = this.usedRow.id;
      let url = `${this.storeUrl.urlUsed}/${id}`;
      const body = JSON.stringify(this.usedRow);
      const config = {
        method: "PUT",
        headers: {
          "content-type": "application/json",
          Authorization: `Bearer ${this.storeLogin.accessToken}`,
        },
        body: body,
      };
      const response = await fetch(url, config);
      this.getfoodWithEverithingById(this.usedRow.foodID);
      this.usedRow.ingredientID = this.usedRow.ingredientID;
      this.usedRow.unit = this.usedRow.unit;
      this.usedRow.quantity = this.usedRow.quantity;
    },

    onClickSearch() {
      this.categoryNameTitle = "Összes";
      if (this.keresoSzo) {
        this.getfoodWithCategroryBySearch();
      } else {
        this.getfoodWithCategrory();
      }
    },

    onClickShowIngredient(id) {
      this.modalFood.show();
      this.currentId = null;
      this.usedRow.foodID = id;
      this.getfoodWithEverithingById(id);
      this.getfoodWithCategroryById(id);
    },
    onClickFilterCategory(id, categoryName) {
      this.getCategoryFilter(id, categoryName);
      this.categoryNameTitle = categoryName;
    },
    onClickCancel() {
      this.modalFood.hide();
      this.usedRow.ingredientID = null;
      this.usedRow.unit = null;
      this.usedRow.quantity = null;
    },
    onClickNewFood() {
      this.getCategory();
      this.modalFood.show();
      this.state = "new";
      this.currentId = null;
      this.editableFood = new Food();
    },
    onClickDeleteFood(id) {
      this.state = "delete";
      this.currentId = id;
      this.deleteFood(id);
    },
    onClicNewIngredient() {
      this.state = "new";
      this.modalIngredient.show();
      this.postUsedRow();
    },
    onClickUpdateIngredient(id) {
      this.state = "edit";
      this.modalIngredient.show();
      this.currentId = id;
      this.getfoodWithCategroryById(id);
      console.log(this.currentId);
    },
    onClickDeleteIngredient(id) {
      if (confirm("Biztosan törölni akarja?")) {
        this.deleteUsed(id);
      }
      console.log(id);
      this.state = "delete";
      this.currentId = id;
    },
    onClickSaveIngredient() {
      // this.form.classList.add("was-validated");
      // if (this.form.checkValidity()) {
      if (this.state == "new") {
        this.postUsedRow();
      } else if (this.state == "edit") {
        this.putUsedRow(this.currentId);
      }
      this.modalIngredient.hide();
      // }
    },
    onClickSaveFood() {
      if (this.state == "new") {
        this.postUsedRow();
      } else if (this.state == "edit") {
        this.putUsedRow(this.currentId);
      }
      this.modalIngredient.hide();
    },
    onClickUpdateFood() {
      this.state = "edit";
    },
    onClicNewFood() {
      this.state = "new";
    },

    getImgUrl(pic) {
      if (pic !== "undefined") {
        return `../../public/${pic}.jpg`;
      } else {
        return `../../public/nopic.jpg`;
      }
    },
    keresJelol(text) {
      if (this.keresoszo) {
        let what = new RegExp(this.keresoszo, "gi");
        text = text.replace(what, (match) => {
          return `<span class="mark">${match}</span>`;
        });
        return text;
      } else {
        return text;
      }
    },
  },
  computed: {
    stateTitleIngredient() {
      if (this.state === "new") {
        return "Új hozzávaló bevitele";
      } else if (this.state === "edit") {
        return "Hozzávaló módosítás";
      }
    },
    stateTitleFood() {
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
.bigLEtter::first-letter {
  text-transform: capitalize;
}
.my-img {
  width: 100%;
  height: 15vw;
  object-fit: cover;
}
table {
  margin-left: auto;
  margin-right: auto;
}
#kategoriak {
  float: right;
  margin-left: auto;
}
#kereso {
  margin-left: auto;
}
.my-up {
  position: absolute;
  bottom: 50%;
  right: 0px;
}
.my-cards {
  position: relative;
}
</style>
