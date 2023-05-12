
<template>
  <div>
    <div class="d-flex align-items-center p-3">
      <h1>Receptek</h1>
      <!-- kereso -->
      <form class="d-flex" role="search" id="kereso">
        <input
          class="form-control me-2"
          type="search"
          placeholder="Search"
          aria-label="Search"
          v-model="keresoSzo"
        />
        <button
          class="btn btn-outline-success"
          @click="onClickSearch()"
          type="button"
        >
          Search
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
            class="card-img-top"
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
            <button
              @click="onClickShowIngredient(food.id)"
              class="btn btn-primary"
            >
              Alapanyagok
            </button>
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
              id="exampleModalLabel"
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
                  <td>{{ food.ingredientName }}</td>
                  <td>{{ food.quantity }}</td>
                  <td>{{ food.unit }}</td>
                </tr>
              </tbody>
            </table>
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
    };
  },
  mounted() {
    this.getfoodWithCategrory();
    this.getCategory();
    this.modal = new bootstrap.Modal(document.getElementById("modalFood"), {
      keyboard: false,
    });
    this.form = document.querySelector(".needs-validation");
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

    onClickSearch() {
      if (this.keresoSzo.trim()) {
        this.getfoodWithCategroryBySearch();
      } else {
        this.getfoodWithCategrory();
      }
    },

    onClickShowIngredient(id) {
      this.modal.show();
      this.currentId = null;
      this.getfoodWithEverithingById(id);
      this.getfoodWithCategroryById(id);
    },
    onClickFilterCategory(id, categoryName) {
      this.getCategoryFilter(id, categoryName);
      this.categoryNameTitle = categoryName;
    },
    onClickCancel() {
      this.modal.hide();
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
};
</script>
<style>
.bigLEtter::first-letter {
  text-transform: capitalize;
}
img {
  max-width: 600px;
  max-height: 350px;
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
