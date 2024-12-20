<template>
  <FSCol
    gap="24px"
  >
    <FSRow
      :width="['50%', '100%', '100%']"
      :wrap="false"
    >
      <FSSearchField
        :hideHeader="true"
        v-model="search"
      />
      <slot
        name="toolbar"
      />
    </FSRow>
    <FSRow
      gap="32px"
    >
      <FSRow
        v-for="(grid, index) in grids"
        :width="width"
        :key="index"
      >
        <FSCol
          gap="16px"
        >
          <FSText
            font="text-h3"
          >
            {{  grid.categoryLabel }}
          </FSText>
          <FSGrid
            :items="grid.items"
          >
            <template
              #item-end="{ item }"
            >
              <FSIconCheck
                :value="getIcon(item.id)"
              />
            </template>
          </FSGrid>
        </FSCol>
      </FSRow>
    </FSRow>
  </FSCol>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, type PropType, ref } from "vue";

import { useBreakpoints } from "@dative-gpi/foundation-shared-components/composables";

import { usePermissionOrganisations, usePermissionOrganisationCategories } from "../composables";

export default defineComponent({
  name: "FEPermissionsGrid",
  props: {
    admin: {
      type: Boolean,
      required: false,
      default: false
    },
    permissions: {
      type: Array as PropType<string[]>,
      required: false,
      default: () => []
    },
    cols: {
      type: Number as PropType<1 | 2>,
      required: false,
      default: 2
    }
  },
  setup(props) {
    const { fetch: getManyPermissionOrganisationCategories, entity: permissionOrganisationCategories } = usePermissionOrganisationCategories();
    const { getMany: getManyPermissionOrganisations, entities: permissionOrganisations } = usePermissionOrganisations();
    const { isExtraSmall } = useBreakpoints();

    const search = ref("");

    const grids = computed(() => {
      if (!permissionOrganisationCategories.value || !permissionOrganisations.value) {
        return [];
      }
      return permissionOrganisationCategories.value.map(pc => ({
        categoryLabel: pc.label,
        categoryCode: pc.prefix,
        items: permissionOrganisations.value.filter(op => op.code.startsWith(pc.prefix)).map(op => ({
          id: op.id,
          label: op.label,
          hideDefault: true
        }))
      })).reduce((acc, grid) => {
        let reducedGrid = { ...grid, items: [...grid.items] };
        if (search.value.length) {
          reducedGrid.items = reducedGrid.items
            .filter(item => JSON.stringify(item).toLowerCase().includes(search.value.toLowerCase()));
        }
        if (reducedGrid.items.length) {
          acc.push(reducedGrid);
        }
        return acc;
      }, []);
    });

    const width = computed(() => {
      return props.cols == 2 && !isExtraSmall.value ? "calc(50% - 16px)" : "100%";
    });

    const getIcon = (permissionId: string): boolean => {
      if (props.admin) {
        return true;
      }
      return props.permissions.some(p => p === permissionId);
    };

    onMounted(() => {
      getManyPermissionOrganisationCategories();
      getManyPermissionOrganisations();
    });

    return {
      search,
      width,
      grids,
      getIcon
    };
  }
});
</script>