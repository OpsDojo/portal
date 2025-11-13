export interface Weight {
  kg: number;
  lbs: number;
}

export interface WeightLog {
  id: string;
  userId: string;
  date: string; // Changed from 'day' to 'date' to match API
  weight: Weight;
  notes?: string;
}

export interface WeightLogLite {
  date: string; // DateOnly expects string in YYYY-MM-DD format
  kg: number;
}
