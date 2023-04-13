
<template>
  <div>
    <h1>Receptek</h1>

    <div class="row row-cols-1 row-cols-md-2 g-4">
      <div class="col"
      v-for="(food, index) in foodWithCategrory"
          :key="`food${index}`">
        <div
          class="card"
         
        >
          <img :src="kep" class="card-img-top" alt="..." />
          <div class="card-body">
            <h5 class="card-title">{{ food.foodName }}</h5>
            <p class="card-text">
              <strong>Az étel feljegyzésének a dátuma:</strong>
              {{ food.descriptionDate }} <br />
              <strong>Az étel első készítésének dátuma:</strong>
              {{ food.firstDate }} <br />
              <strong>Kategória: </strong>
              <span
                v-for="(category, index) in food.category"
                :key="`category${index}`"
              >
                {{ category.categoryName }}
              </span>
            </p>
            <button @click="onClickShowIngredient(food.id)" class="btn btn-primary">Alapanyagok</button>
          </div>
        </div>
      </div>
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
            <h1 class="modal-title fs-5" id="exampleModalLabel">
             foodname
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
            <!--#region Form -->

            <form class="row g-3 needs-validation" novalidate>
              <!-- Autó név -->
              <div class="col-md-12">
                <label for="name" class="form-label">Autó név</label>
                <input
                  type="text"
                  class="form-control"
                  id="name"
                  required
                />
                <div class="invalid-feedback">A név kitöltése kötelező</div>
              </div>

              <!-- Rendszám -->
              <div class="col-md-6">
                <label for="licenceNumber" class="form-label">Rendszám</label>
                <input
                  type="text"
                  class="form-control"
                  id="licenceNumber"
                  required
                />
                <div class="invalid-feedback">
                  A rendszám kitöltése kötelező
                </div>
              </div>

              <!-- Rendszám -->
              <div class="col-md-6">
                <label for="hourlyRate" class="form-label"
                  >Tarifa (Ft/óra)</label
                >
                <input
                  type="number"
                  class="form-control"
                  id="hourlyRate"
                  required
                />
                <div class="invalid-feedback">A tarifa kitöltése kötelező</div>
              </div>

              <!-- out of traffic -->
              <div class="col-md-6">
                <input
                  class="form-check-input"
                  type="checkbox"
                  value=""
                  id="outOfTraffic"
                />
                <label class="form-check-label ms-2" for="flexCheckDefault">
                  Forgalmon kívül
                </label>
              </div>

              
            </form>
            <!--#endregion Form -->
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
import { useUrlStore } from "@/stores/url";
import { useLoginStore } from "@/stores/login";
const storeUrl = useUrlStore();
const storeLogin = useLoginStore();
export default {
  data() {
    return {
      storeUrl,
      storeLogin,
      foodWithCategrory: [],
      kep: "../../public/káposztás tészta.jpg",
      form: null,
      modal: null,
    };
  },
  mounted() {
    this.getfoodWithCategrory();
    this.modal = new bootstrap.Modal(document.getElementById("modalFood"), {
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
    onClickShowIngredient(id){
      this.modal.show();
    },
    onClickCancel() {
      this.modal.hide();
    },
  },
};
</script>
<style>
</style>
