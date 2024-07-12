import { defineComponent, ref, computed, onMounted } from "vue";
import { useExtensionCommunicationBridge } from "@dative-gpi/foundation-extension-shared-ui";
import { useTables } from "../composables";
import { useRouter } from "vue-router";

export default defineComponent({
components: {},
props: {
editMode: {
type: Boolean,
required: true,
},
},
setup(props) {
const extension = useExtensionCommunicationBridge();
const router = useRouter();

const search = ref<string | undefined>();

const { getMany, entities: tables, fetching } = useTables();

const headers = computed(() => {
return [
{
text: "Code",
title: "Code",
value: "code",
sortable: true,
},
{
text: "Name",
title: "name",
value: "label",
}
];
});



const init = () => {
await getMany();
};

const selectTable = (click, row) => {
router.push({
name: "table",
params: { tableId: row.item.id },
});
};

onMounted(init);

return {
headers,
tables,
fetching,
search,
selectTable
};
},
});
